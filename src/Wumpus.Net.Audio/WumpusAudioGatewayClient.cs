using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Voltaic;
using Voltaic.Serialization;
using Wumpus;
using Wumpus.Events;
using Wumpus.Requests;
using Wumpus.Serialization;

namespace Wumpus
{
    public enum ConnectionState
    {
        Disconnected,
        Connecting,
        Connected,
        Disconnecting
    }

    public class WumpusAudioGatewayClient : IDisposable
    {
        public const int ApiVersion = 4;
        public static string Version { get; } =
            typeof(WumpusAudioGatewayClient).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ??
            typeof(WumpusAudioGatewayClient).GetTypeInfo().Assembly.GetName().Version.ToString(3) ??
            "Unknown";

        private const int InitialBackoffMillis = 1000; // 1 second
        private const int MaxBackoffMillis = 60000; // 1 min
        private const double BackoffMultiplier = 1.75; // 1.75x
        private const double BackoffJitter = 0.25; // 1.5 to 2.0x
        private const int ConnectionTimeoutMillis = 30000; // 30 sec
        private const int IdentifyTimeoutMillis = 60000; // 1 min
        // Typical backoff: 1.75s, 3.06s, 5.36s, 9.38s, 16.41s, 28.72s, 50.27s, 60s, 60s...


        // Status events
        public event Action Connected;
        public event Action<Exception> Disconnected;
        public event Action<SerializationException> DeserializationError;

        // Raw events
        public event Action<VoiceGatewayPayload, int> ReceivedPayload;
        public event Action<VoiceGatewayPayload, int> SentPayload;

        // Voice gateway events
        public event Action<VoiceHelloEvent> VoiceGatewayHello;
        public event Action<VoiceReadyEvent> VoiceGatewayReady;
        public event Action VoiceGatewayResumed;
        public event Action<VoiceSessionDescriptionEvent> VoiceSessionDescription;
        public event Action<VoiceSpeakingParams> VoiceSpeaking;
        public event Action VoiceGatewayHeartbeatAck;

        private readonly SemaphoreSlim _stateLock;

        // Instance
        private Task _connectionTask;
        private CancellationTokenSource _runCts;

        // Run (Start/Stop)
        private int _lastSeq;
        private string _endpoint;
        private Utf8String _session;
        private Utf8String _token;

        // Connection (For each WebSocket connection)
        private BlockingCollection<VoiceGatewayPayload> _sendQueue;
        private bool _receivedData;

        public ConnectionState State { get; private set; }
        public WumpusJsonSerializer JsonSerializer { get; }

        public Snowflake UserId { get; }
        public Snowflake GuildId { get; }

        public WumpusAudioGatewayClient(Snowflake userId, Snowflake guildId, WumpusJsonSerializer serializer = null)
        {
            UserId = userId;
            GuildId = guildId;
            JsonSerializer = serializer ?? new WumpusJsonSerializer();
            _stateLock = new SemaphoreSlim(1, 1);
            _connectionTask = Task.CompletedTask;
            _runCts = new CancellationTokenSource();
            _runCts.Cancel(); // Start canceled
        }

        // TODO: Utf8String, string or custom type?
        public void Run(string endpoint, string session, string token)
            => RunAsync(endpoint, session, token).GetAwaiter().GetResult();
        public async Task RunAsync(string endpoint, string session, string token)
        {
            string SlicePort()
            {
                if (endpoint.EndsWith(":80"))
                    return endpoint.Substring(0, endpoint.Length - 3);
                return endpoint;
            }

            Task exceptionSignal;
            await _stateLock.WaitAsync().ConfigureAwait(false);
            try
            {
                await StopAsyncInternal().ConfigureAwait(false);

                _endpoint = SlicePort();
                _runCts = new CancellationTokenSource();
                _session = new Utf8String(session);
                _token = new Utf8String(token);
                _lastSeq = 0;

                _connectionTask = RunTaskAsync(_runCts.Token);
                exceptionSignal = _connectionTask;
            }
            finally
            {
                _stateLock.Release();
            }
            await exceptionSignal.ConfigureAwait(false);
        }
        private async Task RunTaskAsync(CancellationToken runCancelToken)
        {
            Task[] tasks = null;
            bool isRecoverable = true;
            int backoffMillis = InitialBackoffMillis;
            int connectionAttempts = 0;
            var jitter = new Random();

            while (isRecoverable)
            {
                using (var connectionCts = new CancellationTokenSource())
                using (var cancelTokenCts = CancellationTokenSource.CreateLinkedTokenSource(runCancelToken, connectionCts.Token))
                using (var client = new ClientWebSocket())
                {
                    Exception disconnectEx = null;
                    var cancelToken = cancelTokenCts.Token;
                    try
                    {
                        cancelToken.ThrowIfCancellationRequested();
                        var readySignal = new TaskCompletionSource<bool>();
                        _receivedData = true;

                        // Connect
                        State = ConnectionState.Connecting;
                        var uri = new Uri("wss://" + _endpoint + $"/?v={ApiVersion}");
                        await client.ConnectAsync(uri, cancelToken).ConfigureAwait(false);

                        // Receive HELLO (timeout = ConnectionTimeoutMillis)
                        var receiveTask = ReceiveAsync(client, readySignal, cancelToken);
                        await WhenAny(new Task[] { receiveTask }, ConnectionTimeoutMillis,
                            "Timed out waiting for HELLO").ConfigureAwait(false);

                        var evnt = await receiveTask.ConfigureAwait(false);
                        if (!(evnt.Data is VoiceHelloEvent helloEvent))
                            throw new Exception("First event was not a HELLO event");
                        int heartbeatRate = (int)helloEvent.HeartbeatInterval;

                        // Start tasks here since HELLO must be handled before another thread can send/receive
                        _sendQueue = new BlockingCollection<VoiceGatewayPayload>();
                        tasks = new[]
                        {
                            RunSendAsync(client, cancelToken),
                            RunHeartbeatAsync(heartbeatRate, cancelToken),
                            RunReceiveAsync(client, readySignal, cancelToken)
                        };

                        SendIdentify(connectionAttempts == 0);

                        await WhenAny(tasks.Append(readySignal.Task), IdentifyTimeoutMillis,
                            "Timed out waiting for READY or InvalidSession").ConfigureAwait(false);
                        if (await readySignal.Task.ConfigureAwait(false) == false)
                            continue; // Invalid session

                        // Success
                        backoffMillis = InitialBackoffMillis;
                        State = ConnectionState.Connected;
                        Connected?.Invoke();

                        // Wait until an exception occurs (due to cancellation or failure)
                        await WhenAny(tasks).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        disconnectEx = ex;
                        isRecoverable = IsRecoverable(ex);
                        if (!isRecoverable)
                            throw;
                    }
                    finally
                    {
                        var oldState = State;
                        State = ConnectionState.Disconnecting;

                        connectionCts.Cancel();
                        if (tasks != null)
                        {
                            try { await Task.WhenAll(tasks).ConfigureAwait(false); }
                            catch { } // We already captured the root exception
                        }

                        if (client.State == WebSocketState.Open)
                        {
                            try { await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None).ConfigureAwait(false); }
                            catch { }
                        }

                        _sendQueue = null;
                        State = ConnectionState.Disconnected;
                        if (oldState == ConnectionState.Connected)
                            Disconnected?.Invoke(disconnectEx);
                    }
                    if (isRecoverable)
                    {
                        backoffMillis = (int)(backoffMillis * (BackoffMultiplier + (jitter.NextDouble() * BackoffJitter * 2.0 - BackoffJitter)));
                        if (backoffMillis > MaxBackoffMillis)
                            backoffMillis = MaxBackoffMillis;
                        connectionAttempts++;
                        await Task.Delay(backoffMillis).ConfigureAwait(false);
                    }
                }
            }
            _runCts.Cancel();
        }
        private Task RunReceiveAsync(ClientWebSocket client, TaskCompletionSource<bool> readySignal, CancellationToken cancelToken)
        {
            return Task.Run(async () =>
            {
                while (true)
                {
                    cancelToken.ThrowIfCancellationRequested();
                    try
                    {
                        await ReceiveAsync(client, readySignal, cancelToken).ConfigureAwait(false);
                    }
                    catch (SerializationException ex)
                    {
                        DeserializationError?.Invoke(ex);
                    }
                }
            });
        }
        private Task RunSendAsync(ClientWebSocket client, CancellationToken cancelToken)
        {
            return Task.Run(async () =>
            {
                while (true)
                {
                    cancelToken.ThrowIfCancellationRequested();
                    var payload = _sendQueue.Take(cancelToken);
                    await SendAsync(client, cancelToken, payload).ConfigureAwait(false);
                }
            });
        }
        private Task RunHeartbeatAsync(int rate, CancellationToken cancelToken)
        {
            return Task.Run(async () =>
            {
                // extra delay at the beginning because we can only heartbeat after identifying
                await Task.Delay(rate, cancelToken).ConfigureAwait(false);
                while (true)
                {
                    cancelToken.ThrowIfCancellationRequested();
                    if (!_receivedData)
                        throw new TimeoutException("No data was received since the last heartbeat");
                    _receivedData = false;
                    SendHeartbeat();
                    await Task.Delay(rate, cancelToken).ConfigureAwait(false);
                }
            });
        }

        private async Task WhenAny(IEnumerable<Task> tasks)
        {
            var task = await Task.WhenAny(tasks).ConfigureAwait(false);
            await task.ConfigureAwait(false);
        }
        private async Task WhenAny(IEnumerable<Task> tasks, int millis, string errorText)
        {
            var timeoutTask = Task.Delay(millis);
            var task = await Task.WhenAny(tasks.Append(timeoutTask)).ConfigureAwait(false);
            if (task == timeoutTask)
                throw new TimeoutException(errorText);
            await task.ConfigureAwait(false);
        }

        private bool IsRecoverable(Exception ex)
        {
            switch (ex)
            {
                case WebSocketException wsEx:
                    if (wsEx.WebSocketErrorCode == WebSocketError.ConnectionClosedPrematurely)
                        return true;
                    break;
                case WebSocketClosedException wscEx:
                    if (wscEx.CloseStatus.HasValue)
                    {
                        switch (wscEx.CloseStatus.Value)
                        {
                            case WebSocketCloseStatus.Empty:
                            case WebSocketCloseStatus.NormalClosure:
                            case WebSocketCloseStatus.InternalServerError:
                            case WebSocketCloseStatus.ProtocolError:
                                return true;
                        }
                    }
                    else
                    {
                        switch (wscEx.Code)
                        {
                            case 4009:
                            case 4014:
                            case 4015:
                                return true;
                        }
                    }
                    break;
                case TimeoutException _:
                    return true;
            }
            if (ex.InnerException != null)
                return IsRecoverable(ex.InnerException);
            return false;
        }

        public void Stop()
            => StopAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        public async Task StopAsync()
        {
            await _stateLock.WaitAsync().ConfigureAwait(false);
            try
            {
                await StopAsyncInternal().ConfigureAwait(false);
            }
            finally
            {
                _stateLock.Release();
            }
        }
        private async Task StopAsyncInternal()
        {
            _runCts?.Cancel();

            try { await _connectionTask.ConfigureAwait(false); } catch { }
            _connectionTask = Task.CompletedTask;

            var state = State;
            if (state != ConnectionState.Disconnected)
                throw new InvalidOperationException($"Client did not successfully disconnect (State = {state})");
        }

        public void Dispose()
        {
            Stop();
        }

        private async Task<VoiceGatewayPayload> ReceiveAsync(ClientWebSocket client, TaskCompletionSource<bool> readySignal, CancellationToken cancelToken)
        {
            ResizableMemory<byte> wireData = new ResizableMemory<byte>(10 * 1024);
            WebSocketReceiveResult result;
            do
            {
                var buffer = wireData.RequestSegment(10 * 1024);
                result = await client.ReceiveAsync(buffer, cancelToken).ConfigureAwait(false);
                wireData.Advance(result.Count);
                _receivedData = true;

                if (result.CloseStatus != null)
                    throw new WebSocketClosedException(result.CloseStatus.Value, result.CloseStatusDescription);
            }
            while (!result.EndOfMessage);

            var payload = JsonSerializer.Read<VoiceGatewayPayload>(wireData.AsReadOnlySpan());

            HandleEvent(payload, readySignal);
            ReceivedPayload?.Invoke(payload, wireData.Length);
            return payload;
        }
        private void HandleEvent(VoiceGatewayPayload evnt, TaskCompletionSource<bool> readySignal)
        {
            switch (evnt.Operation)
            {
                case VoiceGatewayOperation.Ready:
                    var readyEvent = evnt.Data as VoiceReadyEvent;
                    readySignal.TrySetResult(true);
                    VoiceGatewayReady?.Invoke(readyEvent);
                    break;
                case VoiceGatewayOperation.Resumed:
                    readySignal.TrySetResult(true);
                    VoiceGatewayResumed?.Invoke();
                    break;
                case VoiceGatewayOperation.SessionDescription: VoiceSessionDescription?.Invoke(evnt.Data as VoiceSessionDescriptionEvent); break;
                case VoiceGatewayOperation.Speaking: VoiceSpeaking?.Invoke(evnt.Data as VoiceSpeakingParams); break;
                case VoiceGatewayOperation.HeartbeatAck: VoiceGatewayHeartbeatAck?.Invoke(); break;
                case VoiceGatewayOperation.Hello: VoiceGatewayHello?.Invoke(evnt.Data as VoiceHelloEvent); break;
            }
        }

        public void Send(VoiceGatewayPayload payload)
        {
            if (!_runCts.IsCancellationRequested)
                _sendQueue?.Add(payload);
        }
        private async Task SendAsync(ClientWebSocket client, CancellationToken cancelToken, VoiceGatewayPayload payload)
        {
            var writer = JsonSerializer.Write(payload);
            await client.SendAsync(writer.AsSegment(), WebSocketMessageType.Text, true, cancelToken);
            SentPayload?.Invoke(payload, writer.Length);
        }

        private void SendIdentify(bool identify)
        {
            if (identify) // IDENTIFY
            {
                Send(new VoiceGatewayPayload
                {
                    Operation = VoiceGatewayOperation.Identify,
                    Data = new VoiceIdentifyParams
                    {
                        UserId = UserId,
                        GuildId = GuildId,
                        SessionId = _session,
                        Token = _token
                    }
                });
            }
            else // RESUME
            {
                Send(new VoiceGatewayPayload
                {
                    Operation = VoiceGatewayOperation.Resume,
                    Data = new VoiceResumeParams
                    {
                        GuildId = GuildId,
                        SessionId = _session,
                        Token = _token
                    }
                });
            }
        }
        private void SendHeartbeat() => Send(new VoiceGatewayPayload
        {
            Operation = VoiceGatewayOperation.Heartbeat,
            Data = _lastSeq
        });
    }
}