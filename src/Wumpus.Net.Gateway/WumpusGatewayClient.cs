using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Voltaic;
using Voltaic.Serialization;
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

    public enum StopReason
    {
        Unknown,
        AuthFailed,
        ShardsTooSmall,
        UrlNotFound,
        Exception,
        Canceled
    }

    public class WumpusGatewayClient : IDisposable
    {
        public const int InitialBackoffMillis = 1000; // 1 second
        public const int MaxBackoffMillis = 120000; // 2 mins
        public const double BackoffMultiplier = 1.75; // 1.75x
        public const double BackoffJitter = 0.25; // 1.5x to 2.0x
        public const int ConnectionTimeoutMillis = 20000; // 20 seconds
        
        public event Action Connected;
        public event Action<Exception> Disconnected;
        public event Action<GatewayPayload, ReadOnlyMemory<byte>> ReceivedPayload;
        public event Action<GatewayPayload, ReadOnlyMemory<byte>> SentPayload;

        private static Utf8String LibraryName { get; } = new Utf8String("Wumpus.Net");
        private static Utf8String OsName { get; } =
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? new Utf8String("Windows") :
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? new Utf8String("Linux") :
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? new Utf8String("OSX") :
            new Utf8String("Unknown");

        private readonly WumpusEtfSerializer _serializer;
        private readonly SemaphoreSlim _stateLock;

        // Instance
        private ResizableMemory<byte> _receiveBuffer;
        private Task _connectionTask;
        private CancellationTokenSource _runCts;
        private Utf8String _sessionId;

        // Run (Start/Stop)
        private int _lastSeq;
        private string _url;
        private int? _shardId;
        private int? _totalShards;

        // Connection (For each WebSocket connection)
        private BlockingCollection<GatewayPayload> _sendQueue;

        public AuthenticationHeaderValue Authorization { get; set; }
        public ConnectionState State { get; private set; }
        public Utf8String[] ServerNames { get; private set; }

        public WumpusGatewayClient(WumpusEtfSerializer serializer = null)
        {
            _serializer = serializer ?? new WumpusEtfSerializer();
            _receiveBuffer = new ResizableMemory<byte>(10 * 1024); // 10 KB
            _stateLock = new SemaphoreSlim(1, 1);
            _connectionTask = Task.CompletedTask;
            _runCts = new CancellationTokenSource();
            _runCts.Cancel(); // Start canceled
        }

        public void Run(string url, int? shardId = null, int? totalShards = null, UpdateStatusParams initialPresence = null)
            => RunAsync(url, shardId, totalShards, initialPresence).GetAwaiter().GetResult();
        public async Task RunAsync(string url, int? shardId = null, int? totalShards = null, UpdateStatusParams initialPresence = null)
        {
            Task exceptionSignal;
            await _stateLock.WaitAsync().ConfigureAwait(false);
            try
            {
                await StopAsyncInternal().ConfigureAwait(false);

                _url = url;
                _shardId = shardId;
                _totalShards = totalShards;
                _runCts = new CancellationTokenSource();
                
                _connectionTask = RunTaskAsync(initialPresence, _runCts.Token);
                exceptionSignal = _connectionTask;
            }
            finally
            {
                _stateLock.Release();
            }
            await exceptionSignal.ConfigureAwait(false);
        }
        private async Task RunTaskAsync(UpdateStatusParams initialPresence, CancellationToken runCancelToken)
        {
            Task[] tasks = null;
            bool isRecoverable = true;
            int backoffMillis = InitialBackoffMillis;
            var jitter = new Random();

            while (isRecoverable)
            {
                Exception disconnectEx = null;
                var connectionCts = new CancellationTokenSource();
                var cancelToken = CancellationTokenSource.CreateLinkedTokenSource(runCancelToken, connectionCts.Token).Token;
                using (var client = new ClientWebSocket())
                {
                    try
                    {
                        runCancelToken.ThrowIfCancellationRequested();
                        State = ConnectionState.Connecting;

                        var uri = new Uri(_url + $"?v={DiscordGatewayConstants.APIVersion}&encoding=etf");// &compress=zlib-stream"); // TODO: Enable
                        await client.ConnectAsync(uri, cancelToken).ConfigureAwait(false);

                        // Receive HELLO
                        var evnt = await ReceiveAsync(client, cancelToken); // We should not timeout on receiving HELLO
                        if (!(evnt.Data is HelloEvent helloEvent))
                            throw new Exception("First event was not a HELLO event");
                        int heartbeatRate = helloEvent.HeartbeatInterval;
                        ServerNames = helloEvent.Trace;

                        var readySignal = new TaskCompletionSource<bool>();

                        // Start async loops
                        tasks = new[]
                        {
                            RunSendAsync(client, cancelToken),
                            RunHeartbeatAsync(heartbeatRate, cancelToken),
                            RunReceiveAsync(client, readySignal, cancelToken)
                        };

                        // Send IDENTIFY/RESUME
                        SendIdentify(initialPresence);
                        var timeoutTask = Task.Delay(ConnectionTimeoutMillis);
                        // If anything in tasks fails, it'll throw an exception
                        var task = await Task.WhenAny(tasks.Append(timeoutTask).Append(readySignal.Task)).ConfigureAwait(false);
                        if (task == timeoutTask)
                            throw new TimeoutException("Timed out waiting for READY or InvalidSession");
                        else if (task.IsFaulted)
                            await task.ConfigureAwait(false);

                        // Success
                        _lastSeq = 0;
                        backoffMillis = InitialBackoffMillis;
                        State = ConnectionState.Connected;
                        Connected?.Invoke();

                        task = await Task.WhenAny(tasks).ConfigureAwait(false);
                        if (task.IsFaulted)
                            await task.ConfigureAwait(false);
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

                        // Wait for the other task to complete
                        connectionCts?.Cancel();
                        if (tasks != null)
                        {
                            try { await Task.WhenAll(tasks).ConfigureAwait(false); }
                            catch { } // We already captured the root exception
                        }

                        // receiveTask and sendTask must have completed before we can send/receive from a different thread
                        if (client.State == WebSocketState.Open)
                        {
                            try { await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None).ConfigureAwait(false); }
                            catch { } // We don't actually care if sending a close msg fails
                        }

                        _sessionId = null;
                        ServerNames = null;
                        State = ConnectionState.Disconnected;
                        if (oldState == ConnectionState.Connected)
                            Disconnected?.Invoke(disconnectEx);

                        if (isRecoverable)
                        {
                            backoffMillis = (int)(backoffMillis * (BackoffMultiplier + (jitter.NextDouble() * BackoffJitter * 2.0 - BackoffJitter)));
                            if (backoffMillis > MaxBackoffMillis)
                                backoffMillis = MaxBackoffMillis;
                            await Task.Delay(backoffMillis).ConfigureAwait(false);
                        }
                    }
                }
            }
            _runCts.Cancel(); // Reset to initial canceled state
        }
        private Task RunReceiveAsync(ClientWebSocket client, TaskCompletionSource<bool> readySignal, CancellationToken cancelToken)
        {
            return Task.Run(async () =>
            {
                while (true)
                {
                    cancelToken.ThrowIfCancellationRequested();
                    HandleEvent(await ReceiveAsync(client, cancelToken).ConfigureAwait(false), readySignal);
                }
            });
        }
        private Task RunSendAsync(ClientWebSocket client, CancellationToken cancelToken)
        {
            return Task.Run(async () =>
            {
                try
                {
                    _sendQueue = new BlockingCollection<GatewayPayload>();
                    while (true)
                    {
                        cancelToken.ThrowIfCancellationRequested();
                        var payload = _sendQueue.Take(cancelToken);
                        var writer = _serializer.Write(payload);
                        await client.SendAsync(writer.AsSegment(), WebSocketMessageType.Binary, true, cancelToken);
                        SentPayload?.Invoke(payload, writer.AsReadOnlyMemory());
                    }
                }
                finally
                {
                    _sendQueue = null;
                }
            });
        }
        private Task RunHeartbeatAsync(int rate, CancellationToken cancelToken)
        {
            return Task.Run(async () =>
            {
                while (true)
                {
                    cancelToken.ThrowIfCancellationRequested();
                    SendHeartbeat();
                    await Task.Delay(rate, cancelToken).ConfigureAwait(false);
                }
            });
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
                        // https://discordapp.com/developers/docs/topics/opcodes-and-status-codes#gateway-gateway-close-event-codes
                        switch (wscEx.Code)
                        {
                            case 4000: // Unknown Error
                            case 4008: // Rate Limited // TODO: Handle this better
                            case 4009: // Session timeout
                                return true;
                        }
                    }
                    break;
            }
            return false;
        }

        public void Stop()
            => StopAsync().GetAwaiter().GetResult();
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
            _runCts?.Cancel(); // Cancel any connection attempts or active connections

            try { await _connectionTask.ConfigureAwait(false); } catch { } // Wait for current connection to complete
            _connectionTask = Task.CompletedTask;

            // Double check that the connection task terminated successfully
            var state = State;
            if (state != ConnectionState.Disconnected)
                throw new InvalidOperationException($"Client did not successfully disconnect (State = {state}).");
        }

        public void Dispose()
        {
            Stop();
        }

        private async Task<GatewayPayload> ReceiveAsync(ClientWebSocket client, CancellationToken cancelToken)
        {
            _receiveBuffer.Clear();

            WebSocketReceiveResult result;
            do
            {
                var buffer = _receiveBuffer.GetSegment(10 * 1024); // 10 KB
                result = await client.ReceiveAsync(buffer, cancelToken).ConfigureAwait(false);
                _receiveBuffer.Advance(result.Count);

                if (result.CloseStatus != null)
                    throw new WebSocketClosedException(result.CloseStatus.Value, result.CloseStatusDescription); // TODO: Exception type?
            }
            while (!result.EndOfMessage);

            var payload = _serializer.Read<GatewayPayload>(_receiveBuffer.AsReadOnlySpan());

            if (payload.Sequence.HasValue)
                _lastSeq = payload.Sequence.Value;

            ReceivedPayload?.Invoke(payload, _receiveBuffer.AsReadOnlyMemory());
            return payload;
        }
        private void HandleEvent(GatewayPayload evnt, TaskCompletionSource<bool> readySignal)
        {
            switch (evnt.Operation)
            {
                case GatewayOperation.Dispatch:
                    HandleDispatchEvent(evnt, readySignal);
                    break;
                case GatewayOperation.InvalidSession:
                    if ((bool)evnt.Data != true) // Is resumable
                        _sessionId = null;
                    readySignal.TrySetResult(false);
                    break;
                case GatewayOperation.Heartbeat:
                    SendHeartbeatAck();
                    break;
            }
        }
        private void HandleDispatchEvent(GatewayPayload evnt, TaskCompletionSource<bool> readySignal)
        {
            switch (evnt.DispatchType)
            {
                case GatewayDispatchType.Ready:
                    if (!(evnt.Data is ReadyEvent readyEvent))
                        throw new Exception("Failed to deserialize READY event"); // TODO: Exception type?
                    _sessionId = readyEvent.SessionId;
                    readySignal.TrySetResult(true);
                    break;
            }
        }

        public void Send(GatewayPayload payload)
        {
            if (!_runCts.IsCancellationRequested)
                _sendQueue?.Add(payload);
        }
        private void SendIdentify(UpdateStatusParams initialPresence)
        {
            if (_sessionId == (Utf8String)null) // IDENTITY
            {
                Send(new GatewayPayload
                {
                    Operation = GatewayOperation.Identify,
                    Data = new IdentifyParams
                    {
                        Compress = false, // We don't want payload compression
                        LargeThreshold = 50,
                        Presence = Optional.FromNullable(initialPresence),
                        Properties = new IdentityConnectionProperties
                        {
                            Os = OsName,
                            Browser = LibraryName,
                            Device = LibraryName
                        },
                        Shard = _shardId != null && _totalShards != null ? new int[] { _shardId.Value, _totalShards.Value } : Optional.Create<int[]>(),
                        Token = Authorization != null ? new Utf8String(Authorization.Parameter) : null
                    }
                });
            }
            else // RESUME
            {
                Send(new GatewayPayload
                {
                    Operation = GatewayOperation.Resume,
                    Data = new ResumeParams
                    {
                        Sequence = _lastSeq,
                        SessionId = _sessionId // TODO: Handle READY and get sessionId
                    }
                });
            }
        }
        private void SendHeartbeat() => Send(new GatewayPayload
        {
            Operation = GatewayOperation.Heartbeat,
            Data = _lastSeq == 0 ? (int?)null : _lastSeq
        });
        private void SendHeartbeatAck() => Send(new GatewayPayload
        {
            Operation = GatewayOperation.HeartbeatAck
        });
    }
}
