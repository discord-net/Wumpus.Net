using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Voltaic;
using Voltaic.Serialization.Utf8;

namespace Wumpus
{
    public class WumpusAudioDataClient
    {
        private const int ConnectionTimeoutMillis = 30000;

        public delegate void AudioCallback(ReadOnlySpan<byte> payload);

        // Raw events
        public event AudioCallback ReceivedPayload;
        public event AudioCallback SentPayload;

        // UDP connection events
        public event Action<IPEndPoint> ReceivedLocalEndpoint;

        private readonly SemaphoreSlim _stateLock;

        // Instance
        private Task _connectionTask;
        private CancellationTokenSource _runCts;
        private Socket _socket;

        // Run (Start/Stop)
        private uint _ssrc;
        private IPEndPoint _sendEndpoint;
        private IPEndPoint _receiveEndpoint;

        public WumpusAudioDataClient(ArrayPool<byte> pool = null)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _stateLock = new SemaphoreSlim(1, 1);
            _connectionTask = Task.CompletedTask;
            _runCts = new CancellationTokenSource();
            _runCts.Cancel(); // Start canceled
        }

        public void Run(uint ssrc, IPEndPoint sendAddress)
            => RunAsync(ssrc, sendAddress).GetAwaiter().GetResult();
        public async Task RunAsync(uint ssrc, IPEndPoint sendAddress)
        {
            Task exceptionSignal;
            await _stateLock.WaitAsync().ConfigureAwait(false);
            try
            {
                await StopAsyncInternal().ConfigureAwait(false);

                _ssrc = ssrc;
                _sendEndpoint = sendAddress;
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
            using (var connectionCts = new CancellationTokenSource())
            using (var cancelTokenCts = CancellationTokenSource.CreateLinkedTokenSource(runCancelToken, connectionCts.Token))
            {
                var cancelToken = cancelTokenCts.Token;
                try
                {
                    cancelToken.ThrowIfCancellationRequested();

                    // Perform IP discovery if we do not have our local address cached already
                    if (_receiveEndpoint == null)
                    {
                        await SendDiscoveryAsync().ConfigureAwait(false);
                        var receiveTask = ReceiveDiscoveryAsync();
                        await WhenAny(new Task[] { receiveTask }, ConnectionTimeoutMillis,
                            "Timed out waiting for IP discovery").ConfigureAwait(false);

                        var endpoint = await receiveTask.ConfigureAwait(false);
                        if (endpoint == null)
                            throw new Exception("First receive was not a discovery");

                        _receiveEndpoint = endpoint;
                        ReceivedLocalEndpoint?.Invoke(endpoint);
                    }

                    await RunReceiveAsync(cancelToken).ConfigureAwait(false);
                }
                finally
                {
                    connectionCts.Cancel();
                }
            }
        }
        private async Task WhenAny(IEnumerable<Task> tasks, int millis, string errorText)
        {
            var timeoutTask = Task.Delay(millis);
            var task = await Task.WhenAny(tasks.Append(timeoutTask));
            if (task == timeoutTask)
                throw new TimeoutException(errorText);
            await task.ConfigureAwait(false);
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
        }

        public void Dispose()
        {
            Stop();
            _socket?.Dispose();
        }

        private Task RunReceiveAsync(CancellationToken cancelToken)
        {
            return Task.Run(async () =>
            {
                while (true)
                {
                    cancelToken.ThrowIfCancellationRequested();
                    await ReceiveAsync().ConfigureAwait(false);
                }
            });
        }
        private async Task ReceiveAsync()
        {
            var payload = new ResizableMemory<byte>(10 * 1024);
            var receiveResult = await _socket.ReceiveFromAsync(
                payload.RequestSegment(10 * 1024), SocketFlags.None, _sendEndpoint).ConfigureAwait(false);
            payload.Advance(receiveResult.ReceivedBytes);

            ReceivedPayload?.Invoke(payload.AsReadOnlySpan());
        }

        public async Task SendAsync(ArraySegment<byte> buffer)
        {
            await _socket.SendToAsync(buffer, SocketFlags.None, _sendEndpoint).ConfigureAwait(false);

            SentPayload?.Invoke(buffer.AsSpan());
        }

        private async Task SendDiscoveryAsync()
        {
            var payload = new ResizableMemory<byte>(70);
            BinaryPrimitives.WriteUInt32BigEndian(payload.RequestSpan(70), _ssrc);
            payload.Advance(70);

            await _socket.SendToAsync(
                payload.AsSegment(), SocketFlags.None, _sendEndpoint).ConfigureAwait(false);
        }
        private async Task<IPEndPoint> ReceiveDiscoveryAsync()
        {
            var payload = new ResizableMemory<byte>(70);
            var receiveResult = await _socket.ReceiveFromAsync(
                payload.RequestSegment(70), SocketFlags.None, _sendEndpoint).ConfigureAwait(false);
            payload.Advance(receiveResult.ReceivedBytes);

            return ParseDiscovery(payload.AsReadOnlySpan());

            IPEndPoint ParseDiscovery(ReadOnlySpan<byte> discovery)
            {
                // discovery is always of the form:
                // <4 bytes>, <null-terminated ipv4 address>, <zero padding up to 70 bytes>, <port>
                if (discovery.Length != 70)
                    return null;

                discovery = discovery.Slice(4); // skip ssrc, it's always 0

                // HACK: IPAddress does not have a TryParse which accepts Span<T>
                // so to save on allocations until it does, we use a custom parser
                if (!IPUtilities.TryParseIPv4Address(ref discovery, out var address))
                    return null;

                discovery = discovery.Slice(discovery.Length - 2);
                if (!BinaryPrimitives.TryReadUInt16BigEndian(discovery, out var port))
                    return null;

                return new IPEndPoint(address, port);
            }
        }
    }
}