using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Voltaic;
using Voltaic.Serialization;
using Wumpus.Entities;
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
        public const int ApiVersion = 6;
        public static string Version { get; } =
            typeof(WumpusGatewayClient).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ??
            typeof(WumpusGatewayClient).GetTypeInfo().Assembly.GetName().Version.ToString(3) ??
            "Unknown";

        private const int InitialBackoffMillis = 1000; // 1 second
        private const int MaxBackoffMillis = 60000; // 1 min
        private const double BackoffMultiplier = 1.75; // 1.75x
        private const double BackoffJitter = 0.25; // 1.5x to 2.0x
        private const int ConnectionTimeoutMillis = 30000; // 30 sec
        private const int IdentifyTimeoutMillis = 60000; // 1 min
        // Typical Backoff: 1.75s, 3.06s, 5.36s, 9.38s, 16.41s, 28.72s, 50.27s, 60s, 60s...
        
        // Status events
        public event Action Connected;
        public event Action<Exception> Disconnected;
        public event Action SessionCreated;
        public event Action SessionLost;
        public event Action<SerializationException> DeserializationError;

        // Raw events
        public event Action<GatewayPayload, ReadOnlyMemory<byte>> ReceivedPayload;
        public event Action<GatewayPayload, ReadOnlyMemory<byte>> SentPayload;

        // Gateway events
        public event Action<HelloEvent> GatewayHello;
        public event Action<bool> GatewayInvalidSession;
        public event Action GatewayHeartbeat;
        public event Action GatewayHeartbeatAck;
        public event Action GatewayReconnect;

        // Dispatch events
        public event Action<ReadyEvent> Ready;
        public event Action Resumed;
        public event Action<GatewayGuild> GuildCreate;
        public event Action<Guild> GuildUpdate;
        public event Action<UnavailableGuild> GuildDelete;
        public event Action<Channel> ChannelCreate;
        public event Action<Channel> ChannelUpdate;
        public event Action<Channel> ChannelDelete;
        public event Action<ChannelPinsUpdateEvent> ChannelPinsUpdate;
        public event Action<GuildMemberAddEvent> GuildMemberAdd;
        public event Action<GuildMemberUpdateEvent> GuildMemberUpdate;
        public event Action<GuildMemberRemoveEvent> GuildMemberRemove;
        public event Action<GuildMembersChunkEvent> GuildMembersChunk;
        public event Action<GuildRoleCreateEvent> GuildRoleCreate;
        public event Action<GuildRoleUpdateEvent> GuildRoleUpdate;
        public event Action<GuildRoleDeleteEvent> GuildRoleDelete;
        public event Action<GuildBanAddEvent> GuildBanAdd;
        public event Action<GuildBanRemoveEvent> GuildBanRemove;
        public event Action<GuildEmojiUpdateEvent> GuildEmojisUpdate;
        public event Action<GuildIntegrationsUpdateEvent> GuildIntegrationsUpdate;
        public event Action<WebhooksUpdateEvent> WebhooksUpdate;
        public event Action<Message> MessageCreate;
        public event Action<Message> MessageUpdate;
        public event Action<MessageDeleteEvent> MessageDelete;
        public event Action<MessageDeleteBulkEvent> MessageDeleteBulk;
        public event Action<MessageReactionAddEvent> MessageReactionAdd;
        public event Action<MessageReactionRemoveEvent> MessageReactionRemove;
        public event Action<MessageReactionRemoveAllEvent> MessageReactionRemoveAll;
        public event Action<Presence> PresenceUpdate;
        public event Action<User> UserUpdate;
        public event Action<TypingStartEvent> TypingStart;
        public event Action<VoiceState> VoiceStateUpdate;
        public event Action<VoiceServerUpdateEvent> VoiceServerUpdate;

        private static Utf8String LibraryName { get; } = new Utf8String("Wumpus.Net");
        private static Utf8String OsName { get; } =
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? new Utf8String("Windows") :
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? new Utf8String("Linux") :
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? new Utf8String("OSX") :
            new Utf8String("Unknown");

        private readonly SemaphoreSlim _stateLock;

        // Instance
        private readonly ResizableMemoryStream _compressed, _decompressed;
        private Task _connectionTask;
        private CancellationTokenSource _runCts;
        private Utf8String _sessionId;

        // Run (Start/Stop)
        private int _lastSeq;
        private string _url;
        private int? _shardId;
        private int? _totalShards;

        // Connection (For each WebSocket connection)
        private DeflateStream _zlibStream;
        private BlockingCollection<GatewayPayload> _sendQueue;
        private bool _receivedData;
        private bool _readZlibHeader;

        public AuthenticationHeaderValue Authorization { get; set; }
        public ConnectionState State { get; private set; }
        public Utf8String[] ServerNames { get; private set; }
        public WumpusEtfSerializer EtfSerializer { get; }

        public WumpusGatewayClient(WumpusEtfSerializer serializer = null)
        {
            EtfSerializer = serializer ?? new WumpusEtfSerializer();
            _compressed = new ResizableMemoryStream(10 * 1024); // 10 KB
            _decompressed = new ResizableMemoryStream(10 * 1024); // 10 KB
            _stateLock = new SemaphoreSlim(1, 1);
            _connectionTask = Task.CompletedTask;
            _runCts = new CancellationTokenSource();
            _runCts.Cancel(); // Start canceled
        }

        public void Run(string url, int? shardId = null, int? totalShards = null, UpdateStatusParams initialPresence = null)
            => RunAsync(url, shardId, totalShards, initialPresence).ConfigureAwait(false).GetAwaiter().GetResult();
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
                _sessionId = null;

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
                        var readySignal = new TaskCompletionSource<bool>();
                        _receivedData = true;

                        // Connect
                        State = ConnectionState.Connecting;
                        var uri = new Uri(_url + $"?v={ApiVersion}&encoding=etf&compress=zlib-stream"); // TODO: Enable
                        await client.ConnectAsync(uri, cancelToken).ConfigureAwait(false);

                        _zlibStream = new DeflateStream(_compressed, CompressionMode.Decompress, true);
                        _readZlibHeader = false;

                        // Receive HELLO (timeout = ConnectionTimeoutMillis)
                        var receiveTask = ReceiveAsync(client, readySignal, cancelToken);
                        await WhenAny(new Task[] { receiveTask }, ConnectionTimeoutMillis, 
                            "Timed out waiting for HELLO").ConfigureAwait(false);

                        var evnt = await receiveTask.ConfigureAwait(false);
                        if (!(evnt.Data is HelloEvent helloEvent))
                            throw new Exception("First event was not a HELLO event");
                        int heartbeatRate = helloEvent.HeartbeatInterval;
                        ServerNames = helloEvent.Trace;

                        // Start tasks here since HELLO must be handled before another thread can send/receive
                        _sendQueue = new BlockingCollection<GatewayPayload>();
                        tasks = new[]
                        {
                            RunSendAsync(client, cancelToken),
                            RunHeartbeatAsync(heartbeatRate, cancelToken),
                            RunReceiveAsync(client, readySignal, cancelToken)
                        };

                        // Send IDENTIFY/RESUME (timeout = IdentifyTimeoutMillis)
                        SendIdentify(initialPresence);
                        await WhenAny(tasks.Append(readySignal.Task), IdentifyTimeoutMillis, 
                            "Timed out waiting for READY or InvalidSession").ConfigureAwait(false);
                        if (await readySignal.Task.ConfigureAwait(false) == false)
                            continue; // Invalid session

                        // Success
                        _lastSeq = 0;
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

                        _sendQueue = null;
                        ServerNames = null;
                        State = ConnectionState.Disconnected;
                        if (oldState == ConnectionState.Connected)
                            Disconnected?.Invoke(disconnectEx);
                    }
                    if (isRecoverable)
                    {
                        backoffMillis = (int)(backoffMillis * (BackoffMultiplier + (jitter.NextDouble() * BackoffJitter * 2.0 - BackoffJitter)));
                        if (backoffMillis > MaxBackoffMillis)
                            backoffMillis = MaxBackoffMillis;
                        await Task.Delay(backoffMillis).ConfigureAwait(false);
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
            //if (task.IsFaulted)
            await task.ConfigureAwait(false); // Return or rethrow
        }
        private async Task WhenAny(IEnumerable<Task> tasks, int millis, string errorText)
        {
            var timeoutTask = Task.Delay(millis);
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
                        // https://discordapp.com/developers/docs/topics/opcodes-and-status-codes#gateway-gateway-close-event-codes
                        switch (wscEx.Code)
                        {
                            case 4000: // Unknown Error
                            case 4006: // Invalid Session
                            case 4008: // Rate Limited // TODO: Handle this better
                            case 4009: // Session timeout
                                return true;
                        }
                    }
                    break;
                case TimeoutException _: // Caused by missing heartbeat ack
                    return true;
            }
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

        private async Task<GatewayPayload> ReceiveAsync(ClientWebSocket client, TaskCompletionSource<bool> readySignal, CancellationToken cancelToken)
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
            var payload = EtfSerializer.Read<GatewayPayload>(_decompressed.Buffer.AsReadOnlySpan());
            if (payload.Sequence.HasValue)
                _lastSeq = payload.Sequence.Value;

            // Handle result
            HandleEvent(payload, readySignal); // Must be before event so slow user handling can't trigger our timeouts
            ReceivedPayload?.Invoke(payload, _compressed.Buffer.AsReadOnlyMemory());
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
                        SetSession(null);
                    readySignal.TrySetResult(false);
                    GatewayInvalidSession?.Invoke((bool)evnt.Data);
                    break;
                case GatewayOperation.Heartbeat:
                    SendHeartbeatAck();
                    GatewayHeartbeat?.Invoke();
                    break;
                case GatewayOperation.HeartbeatAck: GatewayHeartbeatAck?.Invoke(); break;
                case GatewayOperation.Hello: GatewayHello?.Invoke(evnt.Data as HelloEvent); break;
                case GatewayOperation.Reconnect: GatewayReconnect?.Invoke(); break;
            }
        }
        private void HandleDispatchEvent(GatewayPayload evnt, TaskCompletionSource<bool> readySignal)
        {
            switch (evnt.DispatchType)
            {
                case GatewayDispatchType.Ready:
                    var readyEvent = evnt.Data as ReadyEvent;
                    SetSession(readyEvent.SessionId);
                    readySignal.TrySetResult(true);
                    Ready?.Invoke(readyEvent);
                    break;
                case GatewayDispatchType.Resumed:
                    readySignal.TrySetResult(true);
                    Resumed?.Invoke();
                    break;
                case GatewayDispatchType.GuildCreate: GuildCreate?.Invoke(evnt.Data as GatewayGuild); break;
                case GatewayDispatchType.GuildUpdate: GuildUpdate?.Invoke(evnt.Data as Guild); break;
                case GatewayDispatchType.GuildDelete: GuildDelete?.Invoke(evnt.Data as UnavailableGuild); break;
                case GatewayDispatchType.ChannelCreate: ChannelCreate?.Invoke(evnt.Data as Channel); break;
                case GatewayDispatchType.ChannelUpdate: ChannelUpdate?.Invoke(evnt.Data as Channel); break;
                case GatewayDispatchType.ChannelDelete: ChannelDelete?.Invoke(evnt.Data as Channel); break;
                case GatewayDispatchType.ChannelPinsUpdate: ChannelPinsUpdate?.Invoke(evnt.Data as ChannelPinsUpdateEvent); break;
                case GatewayDispatchType.GuildMemberAdd: GuildMemberAdd?.Invoke(evnt.Data as GuildMemberAddEvent); break;
                case GatewayDispatchType.GuildMemberUpdate: GuildMemberUpdate?.Invoke(evnt.Data as GuildMemberUpdateEvent); break;
                case GatewayDispatchType.GuildMemberRemove: GuildMemberRemove?.Invoke(evnt.Data as GuildMemberRemoveEvent); break;
                case GatewayDispatchType.GuildMembersChunk: GuildMembersChunk?.Invoke(evnt.Data as GuildMembersChunkEvent); break;
                case GatewayDispatchType.GuildRoleCreate: GuildRoleCreate?.Invoke(evnt.Data as GuildRoleCreateEvent); break;
                case GatewayDispatchType.GuildRoleUpdate: GuildRoleUpdate?.Invoke(evnt.Data as GuildRoleUpdateEvent); break;
                case GatewayDispatchType.GuildRoleDelete: GuildRoleDelete?.Invoke(evnt.Data as GuildRoleDeleteEvent); break;
                case GatewayDispatchType.GuildBanAdd: GuildBanAdd?.Invoke(evnt.Data as GuildBanAddEvent); break;
                case GatewayDispatchType.GuildBanRemove: GuildBanRemove?.Invoke(evnt.Data as GuildBanRemoveEvent); break;
                case GatewayDispatchType.GuildEmojisUpdate: GuildEmojisUpdate?.Invoke(evnt.Data as GuildEmojiUpdateEvent); break;
                case GatewayDispatchType.GuildIntegrationsUpdate: GuildIntegrationsUpdate?.Invoke(evnt.Data as GuildIntegrationsUpdateEvent); break;
                case GatewayDispatchType.MessageCreate: MessageCreate?.Invoke(evnt.Data as Message); break;
                case GatewayDispatchType.MessageUpdate: MessageUpdate?.Invoke(evnt.Data as Message); break;
                case GatewayDispatchType.MessageDelete: MessageDelete?.Invoke(evnt.Data as MessageDeleteEvent); break;
                case GatewayDispatchType.MessageDeleteBulk: MessageDeleteBulk?.Invoke(evnt.Data as MessageDeleteBulkEvent); break;
                case GatewayDispatchType.MessageReactionAdd: MessageReactionAdd?.Invoke(evnt.Data as MessageReactionAddEvent); break;
                case GatewayDispatchType.MessageReactionRemove: MessageReactionRemove?.Invoke(evnt.Data as MessageReactionRemoveEvent); break;
                case GatewayDispatchType.MessageReactionRemoveAll: MessageReactionRemoveAll?.Invoke(evnt.Data as MessageReactionRemoveAllEvent); break;
                case GatewayDispatchType.PresenceUpdate: PresenceUpdate?.Invoke(evnt.Data as Presence); break;
                case GatewayDispatchType.UserUpdate: UserUpdate?.Invoke(evnt.Data as User); break;
                case GatewayDispatchType.TypingStart: TypingStart?.Invoke(evnt.Data as TypingStartEvent); break;
                case GatewayDispatchType.VoiceStateUpdate: VoiceStateUpdate?.Invoke(evnt.Data as VoiceState); break;
                case GatewayDispatchType.VoiceServerUpdate: VoiceServerUpdate?.Invoke(evnt.Data as VoiceServerUpdateEvent); break;
                case GatewayDispatchType.WebhooksUpdate: WebhooksUpdate?.Invoke(evnt.Data as WebhooksUpdateEvent); break;
            }
        }

        public void Send(GatewayPayload payload)
        {
            if (!_runCts.IsCancellationRequested)
                _sendQueue?.Add(payload);
        }
        private async Task SendAsync(ClientWebSocket client, CancellationToken cancelToken, GatewayPayload payload)
        {
            var writer = EtfSerializer.Write(payload);
            await client.SendAsync(writer.AsSegment(), WebSocketMessageType.Binary, true, cancelToken);
            SentPayload?.Invoke(payload, writer.AsReadOnlyMemory());
        }

        private void SendIdentify(UpdateStatusParams initialPresence)
        {
            if (_sessionId is null) // IDENTITY
            {
                Send(new GatewayPayload
                {
                    Operation = GatewayOperation.Identify,
                    Data = new IdentifyParams
                    {
                        Compress = false, // We don't want payload compression
                        LargeThreshold = 50,
                        Presence = Optional.FromObject(initialPresence),
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

        private void SetSession(Utf8String sessionId)
        {
            if (_sessionId != sessionId)
            {
                if (!(_sessionId is null) && sessionId is null)
                    SessionLost?.Invoke();
                else if (_sessionId is null && !(sessionId is null))
                    SessionCreated?.Invoke();
                else
                {
                    SessionLost?.Invoke();
                    SessionCreated?.Invoke();

                }
            }
            _sessionId = sessionId;
        }
    }
}
