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

            Gateway.ReceivedPayload += (msg, data) => ReceivedPayload?.Invoke(msg, data);
            Gateway.SentPayload += (msg, data) => SentPayload?.Invoke(msg, data);

            Gateway.Ready += d => Ready?.Invoke(d);
            Gateway.GuildCreate += d => GuildCreate?.Invoke(d);
            Gateway.GuildUpdate += d => GuildUpdate?.Invoke(d);
            Gateway.GuildDelete += d => GuildDelete?.Invoke(d);
            Gateway.ChannelCreate += d => ChannelCreate?.Invoke(d);
            Gateway.ChannelUpdate += d => ChannelUpdate?.Invoke(d);
            Gateway.ChannelDelete += d => ChannelDelete?.Invoke(d);
            Gateway.ChannelPinsUpdate += d => ChannelPinsUpdate?.Invoke(d);
            Gateway.GuildMemberAdd += d => GuildMemberAdd?.Invoke(d);
            Gateway.GuildMemberUpdate += d => GuildMemberUpdate?.Invoke(d);
            Gateway.GuildMemberRemove += d => GuildMemberRemove?.Invoke(d);
            Gateway.GuildMembersChunk += d => GuildMembersChunk?.Invoke(d);
            Gateway.GuildRoleCreate += d => GuildRoleCreate?.Invoke(d);
            Gateway.GuildRoleUpdate += d => GuildRoleUpdate?.Invoke(d);
            Gateway.GuildRoleDelete += d => GuildRoleDelete?.Invoke(d);
            Gateway.GuildBanAdd += d => GuildBanAdd?.Invoke(d);
            Gateway.GuildBanRemove += d => GuildBanRemove?.Invoke(d);
            Gateway.GuildEmojisUpdate += d => GuildEmojisUpdate?.Invoke(d);
            Gateway.GuildIntegrationsUpdate += d => GuildIntegrationsUpdate?.Invoke(d);
            Gateway.MessageCreate += d => MessageCreate?.Invoke(d);
            Gateway.MessageUpdate += d => MessageUpdate?.Invoke(d);
            Gateway.MessageDelete += d => MessageDelete?.Invoke(d);
            Gateway.MessageDeleteBulk += d => MessageDeleteBulk?.Invoke(d);
            Gateway.MessageReactionAdd += d => MessageReactionAdd?.Invoke(d);
            Gateway.MessageReactionRemove += d => MessageReactionRemove?.Invoke(d);
            Gateway.MessageReactionRemoveAll += d => MessageReactionRemoveAll?.Invoke(d);
            Gateway.PresenceUpdate += d => PresenceUpdate?.Invoke(d);
            Gateway.UserUpdate += d => UserUpdate?.Invoke(d);
            Gateway.TypingStart += d => TypingStart?.Invoke(d);
            Gateway.VoiceStateUpdate += d => VoiceStateUpdate?.Invoke(d);
            Gateway.VoiceServerUpdate += d => VoiceServerUpdate?.Invoke(d);
            Gateway.WebhooksUpdate += d => WebhooksUpdate?.Invoke(d);
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
