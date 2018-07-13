using System;
using System.Collections.Concurrent;
using System.Net.Http.Headers;
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

    public class WumpusGatewayClient : IDisposable
    {
        public event Action Connected;
        public event Action<Exception> Disconnected;
        public event Action<GatewayFrame, ReadOnlyMemory<byte>> ReceivedPayload;
        public event Action<GatewayFrame, ReadOnlyMemory<byte>> SentPayload;

        private static Utf8String LibraryName { get; } = new Utf8String("Wumpus.Net");
        private static Utf8String OsName { get; } =
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? new Utf8String("Windows") :
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? new Utf8String("Linux") :
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? new Utf8String("OSX") :
            new Utf8String("Unknown");

        private readonly WumpusEtfSerializer _serializer;
        private readonly ClientWebSocket _client;
        private readonly SemaphoreSlim _stateLock;

        private ResizableMemory<byte> _receiveBuffer;
        private ConnectionState _state;
        private Task _connectionTask;
        private CancellationTokenSource _connectionCts;
        private BlockingCollection<GatewayFrame> _sendQueue;
        private int _lastSeq;
        private string _lastUrl;
        private int? _lastShardId;
        private int? _lastTotalShards;
        private Utf8String _sessionId;

        public AuthenticationHeaderValue Authorization { get; set; }

        public WumpusGatewayClient(WumpusEtfSerializer serializer = null)
        {
            _serializer = serializer ?? new WumpusEtfSerializer();
            _receiveBuffer = new ResizableMemory<byte>(10 * 1024); // 10 KB
            _client = new ClientWebSocket();
            _stateLock = new SemaphoreSlim(1, 1);
            _connectionTask = Task.CompletedTask;
            _connectionCts = new CancellationTokenSource();
            _sendQueue = new BlockingCollection<GatewayFrame>();
        }

        public async Task ConnectAsync(string url, int? shardId = null, int? totalShards = null, UpdateStatusParams initialPresence = null)
        {
            TaskCompletionSource<bool> connectResult;
            await _stateLock.WaitAsync().ConfigureAwait(false);
            try
            {
                await StopAsync().ConfigureAwait(false);

                _lastUrl = url;
                _lastShardId = shardId;
                _lastTotalShards = totalShards;
                _connectionCts = new CancellationTokenSource();
                _sendQueue = new BlockingCollection<GatewayFrame>();
                connectResult = new TaskCompletionSource<bool>();
                _connectionTask = RunAsync(url, shardId, totalShards, initialPresence, false, _connectionCts.Token, connectResult);
            }
            finally
            {
                _stateLock.Release();
            }

            // Must be outside of stateLock to make sure DisconnectAsync can be called mid-connecting
            await connectResult.Task.ConfigureAwait(false);
        }
        public async Task ResumeAsync()
        {
            TaskCompletionSource<bool> connectResult;
            await _stateLock.WaitAsync().ConfigureAwait(false);
            try
            {
                await StopAsync().ConfigureAwait(false);

                _connectionCts = new CancellationTokenSource();
                _sendQueue = new BlockingCollection<GatewayFrame>();
                connectResult = new TaskCompletionSource<bool>();
                _connectionTask = RunAsync(_lastUrl, _lastShardId, _lastTotalShards, null, true, _connectionCts.Token, connectResult);
            }
            finally
            {
                _stateLock.Release();
            }

            // Must be outside of stateLock to make sure DisconnectAsync can be called mid-connecting
            await connectResult.Task.ConfigureAwait(false);
        }
        public async Task DisconnectAsync()
        {
            await _stateLock.WaitAsync().ConfigureAwait(false);
            try
            {
                await StopAsync().ConfigureAwait(false);
            }
            finally
            {
                _stateLock.Release();
            }
        }

        private async Task RunAsync(string url, int? shardId, int? totalShards, UpdateStatusParams initialPresence, bool isResume, CancellationToken cancelToken, TaskCompletionSource<bool> connectResult)
        {
            // TODO: Add timeout
            int heartbeatRate;
            Task[] tasks = null;
            Exception disconnectEx = null;
            try
            {
                _state = ConnectionState.Connecting;

                var uri = new Uri(url + $"?v={DiscordGatewayConstants.APIVersion}&encoding=etf");// &compress=zlib-stream"); // TODO: Enable
                await _client.ConnectAsync(uri, cancelToken).ConfigureAwait(false);

                // Receive HELLO
                var helloFrame = await ReceiveAsync(cancelToken);
                if (!(helloFrame.Payload is HelloEvent helloEvent))
                    throw new Exception("First frame was not a HELLO frame");
                heartbeatRate = helloEvent.HeartbeatInterval;

                if (!isResume)
                {
                    // Send IDENTITY
                    _sessionId = (Utf8String)null;
                    var identityFrame = new GatewayFrame
                    {
                        Operation = GatewayOpCode.Identify,
                        Payload = new IdentifyParams
                        {
                            Compress = false, // TODO: true
                            LargeThreshold = 50,
                            Presence = Optional.FromNullable(initialPresence),
                            Properties = new IdentityConnectionProperties
                            {
                                Os = OsName,
                                Browser = LibraryName,
                                Device = LibraryName
                            },
                            Shard = shardId != null && totalShards != null ? new int[] { shardId.Value, totalShards.Value } : Optional.Create<int[]>(),
                            Token = Authorization != null ? new Utf8String(Authorization.Parameter) : null
                        }
                    };
                    await SendAsync(identityFrame, cancelToken).ConfigureAwait(false);
                }
                else
                {
                    // Send RESUME
                    var resumeFrame = new GatewayFrame
                    {
                        Operation = GatewayOpCode.Resume,
                        Payload = new ResumeParams
                        {
                            Sequence = _lastSeq,
                            SessionId = _sessionId // TODO: Handle READY and get sessionId
                        }
                    };
                    await SendAsync(resumeFrame, cancelToken).ConfigureAwait(false);
                }

                _lastSeq = 0;
                _state = ConnectionState.Connected;
                Connected?.Invoke();
                connectResult.SetResult(true);

                tasks = new Task[]
                {
                    RunReceiveAsync(cancelToken),
                    RunSendAsync(cancelToken),
                    RunHeartbeatAsync(heartbeatRate, cancelToken)
                };
                await Task.WhenAny(tasks).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                connectResult.TrySetException(ex);
                disconnectEx = ex;
            }
            finally
            { 
                var oldState = _state;
                _state = ConnectionState.Disconnecting;

                // Wait for the other task to complete
                _connectionCts?.Cancel();
                while (_sendQueue.TryTake(out _)) { } // Clear the send queue
                if (tasks != null)
                    await Task.WhenAll(tasks).ConfigureAwait(false);

                // receiveTask and sendTask must have completed before we can send/receive from a different thread
                try { await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None).ConfigureAwait(false); }
                catch { } // We don't actually care if sending a close msg fails
                // TODO: Maybe log it?

                _state = ConnectionState.Disconnected;
                if (oldState == ConnectionState.Connected)
                    Disconnected?.Invoke(disconnectEx);
            }
        }
        private Task RunReceiveAsync(CancellationToken cancelToken)
        {
            return Task.Run(async () =>
            {
                try
                {
                    while (!cancelToken.IsCancellationRequested)
                    {
                        var payload = await ReceiveAsync(cancelToken).ConfigureAwait(false);
                    }
                }
                catch (OperationCanceledException) { } // Ignore
            });
        }
        private Task RunSendAsync(CancellationToken cancelToken)
        {
            return Task.Run(async () =>
            {
                try
                {
                    while (!cancelToken.IsCancellationRequested)
                    {
                        var payload = _sendQueue.Take(cancelToken);
                        await SendAsync(payload, cancelToken).ConfigureAwait(false);
                    }
                }
                catch (OperationCanceledException) { } // Ignore
            });
        }
        private Task RunHeartbeatAsync(int rate, CancellationToken cancelToken)
        {
            return Task.Run(async () =>
            {
                try
                {
                    while (!cancelToken.IsCancellationRequested)
                    {
                        SendHeartbeat();
                        await Task.Delay(rate, cancelToken).ConfigureAwait(false);
                    }
                }
                catch (OperationCanceledException) { } // Ignore
            });
        }

        private async Task StopAsync()
        {
            _connectionCts?.Cancel(); // Cancel any connection attempts or active connections

            await _connectionTask.ConfigureAwait(false); // Wait for current connection to complete

            // Double check that the connection task terminated successfully
            var state = _state;
            if (state != ConnectionState.Disconnected)
                throw new InvalidOperationException($"Client did not successfully disconnect (State = {state}).");
        }

        public void Dispose()
        {
            StopAsync().Wait();
            _client.Dispose();
        }

        public void Send(GatewayFrame payload)
        {
            if (!_connectionCts.IsCancellationRequested)
                _sendQueue.Add(payload);
        }

        private async Task SendAsync(GatewayFrame payload, CancellationToken cancelToken)
        {
            if (!_connectionCts.IsCancellationRequested)
            {
                var writer = _serializer.Write(payload);
                await _client.SendAsync(writer.AsSegment(), WebSocketMessageType.Binary, true, cancelToken);
                SentPayload?.Invoke(payload, writer.AsReadOnlyMemory());
            }
        }

        private async Task<GatewayFrame> ReceiveAsync(CancellationToken cancelToken)
        {
            _receiveBuffer.Clear();

            WebSocketReceiveResult result;
            do
            {
                var buffer = _receiveBuffer.GetSegment(10 * 1024); // 10 KB
                result = await _client.ReceiveAsync(buffer, cancelToken).ConfigureAwait(false);
                _receiveBuffer.Advance(result.Count);

                if (result.CloseStatus != null)
                {
                    if (!string.IsNullOrEmpty(result.CloseStatusDescription))
                        throw new Exception($"WebSocket was closed: {result.CloseStatus.Value} ({result.CloseStatusDescription})"); // TODO: Exception type?
                    else
                        throw new Exception($"WebSocket was closed: {result.CloseStatus.Value}"); // TODO: Exception type?
                }
            }
            while (!result.EndOfMessage);

            var payload = _serializer.Read<GatewayFrame>(_receiveBuffer.AsReadOnlySpan());

            if (payload.Sequence.HasValue)
                _lastSeq = payload.Sequence.Value;

            ReceivedPayload?.Invoke(payload, _receiveBuffer.AsReadOnlyMemory());
            return payload;
        }

        private void HandleFrame(GatewayPayload frame)
        {
            switch (frame.Operation)
            {
                case GatewayOperation.Dispatch:
                    await HandleDispatchEventAsync(frame);
            }
        }

        private void HandleDispatchEvent(GatewayPayload frame)
        {
            switch (frame.DispatchType)
            {
            }
        }

        public void SendHeartbeat() => Send(new GatewayFrame
        {
            Operation = GatewayOpCode.Heartbeat,
            Payload = _lastSeq == 0 ? (int?)null : _lastSeq
        });
    }
}
