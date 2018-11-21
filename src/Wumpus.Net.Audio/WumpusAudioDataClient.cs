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
    public class WumpusAudioDataClient : IDisposable
    {
        private readonly IPEndPoint _endpoint;
        private readonly ArrayPool<byte> _pool;
        private readonly Socket _socket;

        public WumpusAudioDataClient(IPEndPoint endpoint, ArrayPool<byte> pool = null)
        {
            _endpoint = endpoint;
            _pool = pool;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // Bind to any available port
            _socket.Bind(new IPEndPoint(IPAddress.Any, 0));
        }

        public async Task SendAsync(uint ssrc, ushort sequence, uint samplePosition, ArraySegment<byte> audio, Memory<byte> secret, IPEndPoint endpoint = null)
        {
            // TODO: this is broken somewhere. I don't know where.

            endpoint = endpoint ?? _endpoint;

            var memory = new ResizableMemory<byte>(10 * 1024, _pool);
            WriteHeader();
            Encrypt(audio.AsSpan(), secret.Span);

            try
            {
                await _socket.SendToAsync(memory.AsSegment(), SocketFlags.None, endpoint).ConfigureAwait(false);
            }
            finally
            {
                memory.Return();
            }

            void WriteHeader()
            {
                memory.Push(0x80); memory.Push(0x78);

                var header = memory.RequestSpan(10);
                BinaryPrimitives.WriteUInt16BigEndian(header, sequence); // 2 bytes
                BinaryPrimitives.WriteUInt32BigEndian(header.Slice(2), samplePosition); // 4 bytes
                BinaryPrimitives.WriteUInt32BigEndian(header.Slice(6), ssrc); // 4 bytes
                memory.Advance(10);
            }

            void Encrypt(Span<byte> data, Span<byte> key)
            {
                var destinationSize = SodiumPrimitives.ComputeMessageLength(data.Length);
                var destinationSpan = memory.RequestSpan(destinationSize);

                Span<byte> nonce = stackalloc byte[SodiumPrimitives.NonceSize];
                //SodiumPrimitives.GenerateRandomBytes(nonce.Slice(0, 4));

                if (SodiumPrimitives.TryEncryptInPlace(destinationSpan, data, key, nonce))
                    memory.Advance(destinationSize);

                nonce.Slice(0, 4).CopyTo(memory.RequestSpan(4));
                memory.Advance(4);
            }
        }

        public async Task<IPEndPoint> DiscoverAsync(uint ssrc, IPEndPoint endpoint = null)
        {
            endpoint = endpoint ?? _endpoint;

            var memory = new ResizableMemory<byte>(70, _pool);
            BinaryPrimitives.WriteUInt32BigEndian(memory.RequestSpan(70), ssrc);
            memory.Advance(70);

            try
            {
                await _socket.SendToAsync(memory.AsSegment(), SocketFlags.None, endpoint).ConfigureAwait(false);

                var received = await _socket.ReceiveFromAsync(memory.AsSegment(), SocketFlags.None, endpoint).ConfigureAwait(false);

                if (received.ReceivedBytes != 70)
                    throw new Exception("Discovery response was not 70 bytes");

                return ParseDiscovery(memory.AsReadOnlySpan());
            }
            finally
            {
                memory.Return();
            }

            IPEndPoint ParseDiscovery(ReadOnlySpan<byte> discovery)
            {
                if (discovery.Length != 70)
                    return null;

                discovery = discovery.Slice(4); // skip ssrc, it's always 0

                if (!IPUtilities.TryParseIPv4Address(ref discovery, out var address))
                    return null;

                // trim zeros and parse port
                discovery = discovery.Slice(discovery.Length - 2);
                if (!BinaryPrimitives.TryReadUInt16LittleEndian(discovery, out var port))
                    return null;

                return new IPEndPoint(address, port);
            }
        }

        public void Dispose()
        {
            _socket?.Dispose();
        }
    }
}