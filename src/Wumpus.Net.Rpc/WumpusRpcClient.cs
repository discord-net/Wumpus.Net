using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Compression;
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

    public class WumpusRpcClient : IDisposable
    {
        public const int PortRangeStart = 6463;
        public const int PortRangeEnd = 6472;

        public const int InitialBackoffMillis = 1000; // 1 second
        public const int MaxBackoffMillis = 60000; // 1 min
        public const double BackoffMultiplier = 1.75; // 1.75x
        public const double BackoffJitter = 0.25; // 1.5x to 2.0x
        public const int ConnectionTimeoutMillis = 30000; // 30 sec
        public const int IdentifyTimeoutMillis = 60000; // 1 min
        // Typical Backoff: 1.75s, 3.06s, 5.36s, 9.38s, 16.41s, 28.72s, 50.27s, 60s, 60s...
        
        public event Action Connected;
        public event Action<Exception> Disconnected;
        public event Action<RpcPayload, ReadOnlyMemory<byte>> ReceivedPayload;
        public event Action<RpcPayload, ReadOnlyMemory<byte>> SentPayload;

        private readonly WumpusEtfSerializer _serializer;
        private readonly SemaphoreSlim _stateLock;

        // Instance
        private readonly ResizableMemoryStream _compressed, _decompressed;
        private Task _connectionTask;
        private CancellationTokenSource _runCts;

        // Run (Start/Stop)
        private string _url;
        private string _clientId;

        // Connection (For each WebSocket connection)
        private DeflateStream _zlibStream;
        private BlockingCollection<RpcPayload> _sendQueue;
        private bool _readZlibHeader;

        public AuthenticationHeaderValue Authorization { get; set; }
        public ConnectionState State { get; private set; }
        public Utf8String[] ServerNames { get; private set; }

        public WumpusRpcClient(WumpusEtfSerializer serializer = null)
        {
            _serializer = serializer ?? new WumpusEtfSerializer();            
            _compressed = new ResizableMemoryStream(10 * 1024); // 10 KB
            _decompressed = new ResizableMemoryStream(10 * 1024); // 10 KB
            _stateLock = new SemaphoreSlim(1, 1);
            _connectionTask = Task.CompletedTask;
            _runCts = new CancellationTokenSource();
            _runCts.Cancel(); // Start canceled
        }

        public void Run(string clientId)
            => RunAsync(clientId).GetAwaiter().GetResult();
        public async Task RunAsync(string clientId)
        {
            Task exceptionSignal;
            await _stateLock.WaitAsync().ConfigureAwait(false);
            try
            {
                await StopAsyncInternal().ConfigureAwait(false);

                _url = null;
                _clientId = clientId;
                _runCts = new CancellationTokenSource();
                
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
                        var readySignal = new TaskCompletionSource<bool>();

                        // Connect
                        State = ConnectionState.Connecting;

                        if (_url == null)
                        {
                            // Search for RPC server
                            for (int port = PortRangeStart; port <= PortRangeEnd; port++)
                            {
                                try
                                {
                                    string url = $"ws://127.0.0.1:{port}";
                                    var uri = new Uri(url + $"?v={DiscordRpcConstants.APIVersion}&client_id={_clientId}&encoding=etf&compress=zlib-stream");
                                    await client.ConnectAsync(uri, cancelToken).ConfigureAwait(false);
                                    _url = url;
                                    break;
                                }
                                catch (Exception) { }
                            }
                            if (_url == null)
                                throw new Exception("Failed to locate local RPC server");
                        }
                        else
                        {
                            // Reconnect to previously found server
                            var uri = new Uri(_url + $"?v={DiscordRpcConstants.APIVersion}&client_id={_clientId}&encoding=etf&compress=zlib-stream");
                            await client.ConnectAsync(uri, cancelToken).ConfigureAwait(false);                                        
                        }

                        _zlibStream = new DeflateStream(_compressed, CompressionMode.Decompress, true);
                        _readZlibHeader = false;

                        await SendAsync(new RpcPayload
                        {
                            Cmd = RpcCommand.Authorize,
                            Args = new AuthorizeParams
                            {
                                ClientId = _clientId,
                                Scopes = _scopes
                            }
                        }).ConfigureAwait(false);
                        var receiveTask = ReceiveAsync(client, readySignal, cancelToken);
                        await WhenAny(new Task[] { receiveTask }, ConnectionTimeoutMillis, 
                            "Timed out waiting for AUTHORIZE response").ConfigureAwait(false);
                        var evnt = await receiveTask.ConfigureAwait(false);
                        if (!(evnt.Data is AuthorizeResponse authorizeResponse))
                            throw new Exception("Authorize response was not a AUTHORIZE cmd");


                        receiveTask = ReceiveAsync(client, readySignal, cancelToken);
                        await WhenAny(new Task[] { receiveTask }, ConnectionTimeoutMillis, 
                            "Timed out waiting for HELLO").ConfigureAwait(false);
                        evnt = await receiveTask.ConfigureAwait(false);
                        if (!(evnt.Data is AuthenticateResponse authenticateResponse))
                            throw new Exception("Authenticate response was not a AUTHENTICATE cmd");

                        // Start tasks here since HELLO must be handled before another thread can send/receive
                        tasks = new[]
                        {
                            RunSendAsync(client, cancelToken),
                            RunReceiveAsync(client, readySignal, cancelToken)
                        };

                        // Send IDENTIFY/RESUME (timeout = IdentifyTimeoutMillis)
                        SendIdentify(initialPresence);
                        timeoutTask = Task.Delay(IdentifyTimeoutMillis);
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

                        // Wait until an exception occurs (due to cancellation or failure)
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

                        // Wait for the other tasks to complete
                        connectionCts.Cancel();
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
                    await ReceiveAsync(client, readySignal, cancelToken).ConfigureAwait(false);
                }
            });
        }
        private Task RunSendAsync(ClientWebSocket client, CancellationToken cancelToken)
        {
            return Task.Run(async () =>
            {
                try
                {
                    _sendQueue = new BlockingCollection<RpcPayload>();
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

        private async Task WhenAny(IEnumerable<Task> tasks)
        {
            var task = await Task.WhenAny(tasks).ConfigureAwait(false);
            //if (task.IsFaulted)
            await task.ConfigureAwait(false); // Return or rethrow
        }
        private async Task WhenAny(IEnumerable<Task> tasks, int millis, string errorText)
        {
            var timeoutTask = Task.Delay(ConnectionTimeoutMillis);
            var task = await Task.WhenAny(tasks.Append(timeoutTask)).ConfigureAwait(false);
            if (task == timeoutTask)
                throw new TimeoutException(errorText);
            //else if (task.IsFaulted)
            await task.ConfigureAwait(false); // Return or rethrow
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
                        // https://discordapp.com/developers/docs/topics/opcodes-and-status-codes#rpc-rpc-close-event-codes
                        switch (wscEx.Code)
                        {
                            case 4002: // Rate Limited // TODO: Handle this better
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

        private async Task<RpcPayload> ReceiveAsync(ClientWebSocket client, TaskCompletionSource<bool> readySignal, CancellationToken cancelToken)
        {
            // Reset compressed stream
            _compressed.Position = 0;
            _compressed.SetLength(0);

            // Receive compressed data
            WebSocketReceiveResult result;
            do
            {
                var buffer = _compressed.Buffer.RequestSegment(10 * 1024); // 10 KB
                result = await client.ReceiveAsync(buffer, cancelToken).ConfigureAwait(false);
                _compressed.Buffer.Advance(result.Count);
                _receivedData = true;

                if (result.CloseStatus != null)
                    throw new WebSocketClosedException(result.CloseStatus.Value, result.CloseStatusDescription);
            }
            while (!result.EndOfMessage);

            // Skip zlib header
            if (!_readZlibHeader)
            {
                if (_compressed.Buffer.Array[0] != 0x78)
                    throw new SerializationException("First payload is missing zlib header");
                _compressed.Position = 2;
                _readZlibHeader = true;
            }

            // Reset decompressed stream
            _decompressed.Position = 0;
            _decompressed.SetLength(0);

            // Decompress
            _zlibStream.CopyTo(_decompressed);

            // Deserialize
            var payload = EtfSerializer.Read<RpcPayload>(_decompressed.Buffer.AsReadOnlySpan());
            if (payload.Sequence.HasValue)
                _lastSeq = payload.Sequence.Value;

            // Handle result
            HandleEvent(payload, readySignal); // Must be before event so slow user handling can't trigger our timeouts
            ReceivedPayload?.Invoke(payload, _compressed.Buffer.AsReadOnlyMemory());
            return payload;
        }
        private void HandleEvent(RpcPayload evnt, TaskCompletionSource<bool> readySignal)
        {
            // switch (evnt.Operation)
            // {
            //     case GatewayOperation.Dispatch:
            //         HandleDispatchEvent(evnt, readySignal);
            //         break;
            //     case GatewayOperation.InvalidSession:
            //         if ((bool)evnt.Data != true) // Is resumable
            //             _sessionId = null;
            //         readySignal.TrySetResult(false);
            //         break;
            //     case GatewayOperation.Heartbeat:
            //         SendHeartbeatAck();
            //         break;
            // }
        }

        public void Send(RpcPayload payload)
        {
            if (!_runCts.IsCancellationRequested)
                _sendQueue?.Add(payload);
        }
    }
}
