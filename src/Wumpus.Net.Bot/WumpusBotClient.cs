using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Voltaic.Logging;
using Wumpus.Entities;
using Wumpus.Events;
using Wumpus.Net;
using Wumpus.Requests;
using Wumpus.Responses;
using Wumpus.Serialization;

namespace Wumpus
{
    public partial class WumpusBotClient
    {
        public static string Version { get; } =
            typeof(WumpusBotClient).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ??
            typeof(WumpusBotClient).GetTypeInfo().Assembly.GetName().Version.ToString(3) ??
            "Unknown";

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
        public event Action<WebhooksUpdateEvent> WebhooksUpdate;

        public WumpusRestClient Rest { get; }
        public WumpusGatewayClient Gateway { get; }

        private readonly LogManager _logManager;
        private readonly ILogger _logger;
        private bool _wroteInitialLog;
        private SemaphoreSlim _runLock;

        public AuthenticationHeaderValue Authorization
        {
            get => Rest.Authorization;
            set
            {
                Rest.Authorization = value;
                Gateway.Authorization = value;
            }
        }

        public WumpusBotClient(
            WumpusJsonSerializer jsonSerializer = null, WumpusEtfSerializer etfSerializer = null, 
            IRateLimiter restRateLimiter = null, 
            LogManager logManager = null, bool logLibraryInfo = true)
        {
            Rest = new WumpusRestClient(jsonSerializer, restRateLimiter);
            Gateway = new WumpusGatewayClient(etfSerializer);
            _runLock = new SemaphoreSlim(1, 1);

            if (logManager != null)
            {
                _logManager = logManager;
                _logger = _logManager?.CreateLogger("Wumpus.Net") ?? new NullLogger();
                _wroteInitialLog = !logLibraryInfo;
                if (logManager.MinSeverity >= LogSeverity.Info)
                {
                    Gateway.Connected += () => _logger.Info("Connected to gateway");
                    Gateway.Disconnected += ex => _logger.Info("Disconnected to gateway", ex);
                }
                if (logManager.MinSeverity >= LogSeverity.Verbose)
                {
                    Rest.JsonSerializer.UnknownProperty += path => _logger.Verbose($"Unknown JSON property \"{path}\"");
                    Rest.JsonSerializer.FailedProperty += path => _logger.Verbose($"Failed to deserialize JSON \"{path}\"");
                    Gateway.EtfSerializer.UnknownProperty += path => _logger.Verbose($"Unknown ETF property \"{path}\"");
                    Gateway.EtfSerializer.FailedProperty += path => _logger.Verbose($"Failed to deserialize ETF \"{path}\"");
                }
                if (logManager.MinSeverity >= LogSeverity.Debug)
                {
                    Gateway.ReceivedPayload += (payload, bytes) =>
                    {
                        if (payload.Operation == GatewayOperation.Dispatch)
                            _logger.Debug($"<~ {payload.DispatchType.Value} ({bytes.Length} bytes)");
                        else
                            _logger.Debug($"<~ {payload.Operation} ({bytes.Length} bytes)");
                    };
                    Gateway.SentPayload += (payload, bytes) =>
                    {
                        if (payload.Operation == GatewayOperation.Dispatch)
                            _logger.Debug($"~> {payload.DispatchType.Value} ({bytes.Length} bytes)");
                        else
                            _logger.Debug($"~> {payload.Operation} ({bytes.Length} bytes)");
                    };
                }
            }
            else
            {
                _logger = new NullLogger();
                _wroteInitialLog = true;
            }

            Gateway.ReceivedPayload += (msg, data) =>
            {
                ReceivedPayload?.Invoke(msg, data);
                switch (msg.Operation)
                {
                    case GatewayOperation.Heartbeat: GatewayHeartbeat?.Invoke(); break;
                    case GatewayOperation.HeartbeatAck: GatewayHeartbeatAck?.Invoke(); break;
                    case GatewayOperation.Hello: GatewayHello?.Invoke(msg.Data as HelloEvent); break;
                    case GatewayOperation.InvalidSession: GatewayInvalidSession?.Invoke((bool)msg.Data); break;
                    case GatewayOperation.Reconnect: GatewayReconnect?.Invoke(); break;
                    case GatewayOperation.Dispatch:
                        switch (msg.DispatchType)
                        {
                            case GatewayDispatchType.Ready: Ready?.Invoke(msg.Data as ReadyEvent); break;
                            case GatewayDispatchType.GuildCreate: GuildCreate?.Invoke(msg.Data as GatewayGuild); break;
                            case GatewayDispatchType.GuildUpdate: GuildUpdate?.Invoke(msg.Data as Guild); break;
                            case GatewayDispatchType.GuildDelete: GuildDelete?.Invoke(msg.Data as UnavailableGuild); break;
                            case GatewayDispatchType.ChannelCreate: ChannelCreate?.Invoke(msg.Data as Channel); break;
                            case GatewayDispatchType.ChannelUpdate: ChannelUpdate?.Invoke(msg.Data as Channel); break;
                            case GatewayDispatchType.ChannelDelete: ChannelDelete?.Invoke(msg.Data as Channel); break;
                            case GatewayDispatchType.ChannelPinsUpdate: ChannelPinsUpdate?.Invoke(msg.Data as ChannelPinsUpdateEvent); break;
                            case GatewayDispatchType.GuildMemberAdd: GuildMemberAdd?.Invoke(msg.Data as GuildMemberAddEvent); break;
                            case GatewayDispatchType.GuildMemberUpdate: GuildMemberUpdate?.Invoke(msg.Data as GuildMemberUpdateEvent); break;
                            case GatewayDispatchType.GuildMemberRemove: GuildMemberRemove?.Invoke(msg.Data as GuildMemberRemoveEvent); break;
                            case GatewayDispatchType.GuildMembersChunk: GuildMembersChunk?.Invoke(msg.Data as GuildMembersChunkEvent); break;
                            case GatewayDispatchType.GuildRoleCreate: GuildRoleCreate?.Invoke(msg.Data as GuildRoleCreateEvent); break;
                            case GatewayDispatchType.GuildRoleUpdate: GuildRoleUpdate?.Invoke(msg.Data as GuildRoleUpdateEvent); break;
                            case GatewayDispatchType.GuildRoleDelete: GuildRoleDelete?.Invoke(msg.Data as GuildRoleDeleteEvent); break;
                            case GatewayDispatchType.GuildBanAdd: GuildBanAdd?.Invoke(msg.Data as GuildBanAddEvent); break;
                            case GatewayDispatchType.GuildBanRemove: GuildBanRemove?.Invoke(msg.Data as GuildBanRemoveEvent); break;
                            case GatewayDispatchType.GuildEmojisUpdate: GuildEmojisUpdate?.Invoke(msg.Data as GuildEmojiUpdateEvent); break;
                            case GatewayDispatchType.GuildIntegrationsUpdate: GuildIntegrationsUpdate?.Invoke(msg.Data as GuildIntegrationsUpdateEvent); break;
                            case GatewayDispatchType.MessageCreate: MessageCreate?.Invoke(msg.Data as Message); break;
                            case GatewayDispatchType.MessageUpdate: MessageUpdate?.Invoke(msg.Data as Message); break;
                            case GatewayDispatchType.MessageDelete: MessageDelete?.Invoke(msg.Data as MessageDeleteEvent); break;
                            case GatewayDispatchType.MessageDeleteBulk: MessageDeleteBulk?.Invoke(msg.Data as MessageDeleteBulkEvent); break;
                            case GatewayDispatchType.MessageReactionAdd: MessageReactionAdd?.Invoke(msg.Data as MessageReactionAddEvent); break;
                            case GatewayDispatchType.MessageReactionRemove: MessageReactionRemove?.Invoke(msg.Data as MessageReactionRemoveEvent); break;
                            case GatewayDispatchType.MessageReactionRemoveAll: MessageReactionRemoveAll?.Invoke(msg.Data as MessageReactionRemoveAllEvent); break;
                            case GatewayDispatchType.PresenceUpdate: PresenceUpdate?.Invoke(msg.Data as Presence); break;
                            case GatewayDispatchType.UserUpdate: UserUpdate?.Invoke(msg.Data as User); break;
                            case GatewayDispatchType.TypingStart: TypingStart?.Invoke(msg.Data as TypingStartEvent); break;
                            case GatewayDispatchType.VoiceStateUpdate: VoiceStateUpdate?.Invoke(msg.Data as VoiceState); break;
                            case GatewayDispatchType.VoiceServerUpdate: VoiceServerUpdate?.Invoke(msg.Data as VoiceServerUpdateEvent); break;
                            case GatewayDispatchType.WebhooksUpdate: WebhooksUpdate?.Invoke(msg.Data as WebhooksUpdateEvent); break;
                        }
                        break;
                }
            };
            Gateway.SentPayload += (msg, data) => SentPayload?.Invoke(msg, data);
        }

        public void Run(string url = null, int? shardId = null, int? totalShards = null, UpdateStatusParams initialPresence = null)
            => RunAsync(url, shardId, totalShards, initialPresence).GetAwaiter().GetResult();
        public async Task RunAsync(string url = null, int? shardId = null, int? totalShards = null, UpdateStatusParams initialPresence = null)
        {
            await _runLock.WaitAsync().ConfigureAwait(false);
            try
            {
                if (!_wroteInitialLog)
                {
                    _logger.Info($"Wumpus.Net.Bot v{Version}");
                    _logger.Info($"Wumpus.Net.Rest v{WumpusRestClient.Version} (API v{WumpusRestClient.ApiVersion})");
                    _logger.Info($"Wumpus.Net.Gateway v{WumpusGatewayClient.Version} (API v{WumpusGatewayClient.ApiVersion})");
                    _wroteInitialLog = true;
                }
                while (true)
                {
                    GetGatewayResponse gatewayInfo = null;
                    if (url == null)
                    {
                        try
                        {
                            gatewayInfo = await Rest.GetGatewayAsync();
                        }
                        catch (HttpRequestException ex)
                        {
                            _logger.Warning("Failed to get gateway URL", ex);
                            await Task.Delay(5000);
                        }
                    }
                    try
                    {
                        await Gateway.RunAsync(url ?? gatewayInfo.Url.ToString(), shardId, totalShards, initialPresence);
                    }
                    catch (WebSocketException ex) when (ex.InnerException is HttpRequestException)
                    {
                        _logger.Warning("Failed to connect to gateway URL", ex);
                        await Task.Delay(5000);
                    }
                }
            }
            finally
            {
                _runLock.Release();
            }
        }

        public void Stop()
            => Gateway.Stop();
        public async Task StopAsync()
        {
            await Gateway.StopAsync();
        }
    }
}
