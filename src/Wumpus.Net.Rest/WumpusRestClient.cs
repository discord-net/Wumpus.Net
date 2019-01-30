using RestEase;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Voltaic;
using Wumpus.Entities;
using Wumpus.Net;
using Wumpus.Requests;
using Wumpus.Responses;
using Wumpus.Serialization;

namespace Wumpus
{
    public class WumpusRestClient : IDiscordRestApi, IDisposable
    {
        public const int ApiVersion = 6;
        public const string ReasonHeader = "X-Audit-Log-Reason";
        public static string Version { get; } =
            typeof(WumpusRestClient).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ??
            typeof(WumpusRestClient).GetTypeInfo().Assembly.GetName().Version.ToString(3) ??
            "Unknown";

        private readonly IDiscordRestApi _api;

        public AuthenticationHeaderValue Authorization { get => _api.Authorization; set => _api.Authorization = value; }
        public WumpusJsonSerializer JsonSerializer { get; }

        public WumpusRestClient(WumpusJsonSerializer serializer = null, IRateLimiter rateLimiter = null)
            : this($"https://discordapp.com/api/v{ApiVersion}/", serializer) { }
        public WumpusRestClient(string url, WumpusJsonSerializer serializer = null, IRateLimiter rateLimiter = null)
        {
            JsonSerializer = serializer ?? new WumpusJsonSerializer();
            rateLimiter = rateLimiter ?? new DefaultRateLimiter();

            var httpClient = new HttpClient { BaseAddress = new Uri(url) };
            httpClient.DefaultRequestHeaders.Add("User-Agent", $"DiscordBot (https://github.com/RogueException/Wumpus.Net, v{Version})");

            _api = RestClient.For<IDiscordRestApi>(new WumpusRequester(httpClient, JsonSerializer, rateLimiter));
        }
        public void Dispose() => _api.Dispose();

        // Audit Log

        public Task<AuditLog> GetGuildAuditLogAsync(Snowflake guildId, GetGuildAuditLogParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.GetGuildAuditLogAsync(guildId, args);
        }

        // Channel

        public Task<Channel> GetChannelAsync(Snowflake channelId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            return _api.GetChannelAsync(channelId);
        }
        public Task<Channel> ReplaceChannelAsync(Snowflake channelId, ModifyChannelParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ReplaceChannelAsync(channelId, args);
        }
        public Task<Channel> ModifyChannelAsync(Snowflake channelId, ModifyChannelParams args, string reason = null)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyChannelAsync(channelId, args);
        }
        public Task<Channel> DeleteChannelAsync(Snowflake channelId, string reason = null)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            return _api.DeleteChannelAsync(channelId);
        }

        public Task<Message[]> GetChannelMessagesAsync(Snowflake channelId, GetChannelMessagesParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.GetChannelMessagesAsync(channelId, args);
        }
        public Task<Message> GetChannelMessageAsync(Snowflake channelId, Snowflake messageId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            return _api.GetChannelMessageAsync(channelId, messageId);
        }
        public Task<Message> CreateMessageAsync(Snowflake channelId, CreateMessageParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.CreateMessageAsync(channelId, args);
        }
        public Task<Message> ModifyMessageAsync(Snowflake channelId, Snowflake messageId, ModifyMessageParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyMessageAsync(channelId, messageId, args);
        }
        public Task DeleteMessageAsync(Snowflake channelId, Snowflake messageId, string reason = null)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            return _api.DeleteMessageAsync(channelId, messageId);
        }
        public Task DeleteMessagesAsync(Snowflake channelId, DeleteMessagesParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.DeleteMessagesAsync(channelId, args);
        }

        public Task<User[]> GetReactionsAsync(Snowflake channelId, Snowflake messageId, Utf8String emoji)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            Preconditions.NotNull(emoji, nameof(emoji));
            return _api.GetReactionsAsync(channelId, messageId, emoji);
        }
        public Task CreateReactionAsync(Snowflake channelId, Snowflake messageId, Utf8String emoji)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            Preconditions.NotNull(emoji, nameof(emoji));
            return _api.CreateReactionAsync(channelId, messageId, emoji);
        }
        public Task DeleteReactionAsync(Snowflake channelId, Snowflake messageId, Utf8String emoji)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            Preconditions.NotNull(emoji, nameof(emoji));
            return _api.DeleteReactionAsync(channelId, messageId, emoji);
        }
        public Task DeleteReactionAsync(Snowflake channelId, Snowflake messageId, Snowflake userId, Utf8String emoji)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            Preconditions.NotZero(userId, nameof(userId));
            Preconditions.NotNull(emoji, nameof(emoji));
            return _api.DeleteReactionAsync(channelId, messageId, userId, emoji);
        }
        public Task DeleteAllReactionsAsync(Snowflake channelId, Snowflake messageId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            return _api.DeleteAllReactionsAsync(channelId, messageId);
        }

        public Task EditChannelPermissionsAsync(Snowflake channelId, Snowflake overwriteId, ModifyChannelPermissionsParams args, string reason = null)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(overwriteId, nameof(overwriteId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.EditChannelPermissionsAsync(channelId, overwriteId, args);
        }
        public Task DeleteChannelPermissionsAsync(Snowflake channelId, Snowflake overwriteId, string reason = null)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(overwriteId, nameof(overwriteId));
            return _api.DeleteChannelPermissionsAsync(channelId, overwriteId);
        }

        public Task<Invite[]> GetChannelInvitesAsync(Snowflake channelId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            return _api.GetChannelInvitesAsync(channelId);
        }
        public Task<Invite> CreateChannelInviteAsync(Snowflake channelId, CreateChannelInviteParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.CreateChannelInviteAsync(channelId, args);
        }

        public Task<Message[]> GetPinnedMessagesAsync(Snowflake channelId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            return _api.GetPinnedMessagesAsync(channelId);
        }
        public Task PinMessageAsync(Snowflake channelId, Snowflake messageId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            return _api.PinMessageAsync(channelId, messageId);
        }
        public Task UnpinMessageAsync(Snowflake channelId, Snowflake messageId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            return _api.UnpinMessageAsync(channelId, messageId);
        }

        public Task AddRecipientAsync(Snowflake channelId, Snowflake userId, AddChannelRecipientParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(userId, nameof(userId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.AddRecipientAsync(channelId, userId, args);
        }
        public Task RemoveRecipientAsync(Snowflake channelId, Snowflake userId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(userId, nameof(userId));
            return _api.RemoveRecipientAsync(channelId, userId);
        }

        public Task TriggerTypingIndicatorAsync(Snowflake channelId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            return _api.TriggerTypingIndicatorAsync(channelId);
        }

        // Emoji

        public Task<Emoji[]> GetGuildEmojisAsync(Snowflake guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.GetGuildEmojisAsync(guildId);
        }
        public Task<Emoji> GetGuildEmojiAsync(Snowflake guildId, Snowflake emojiId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(emojiId, nameof(emojiId));
            return _api.GetGuildEmojiAsync(guildId, emojiId);
        }
        public Task<Emoji> CreateGuildEmojiAsync(Snowflake guildId, CreateGuildEmojiParams args, string reason = null)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            return _api.CreateGuildEmojiAsync(guildId, args);
        }
        public Task<Emoji> ModifyGuildEmojiAsync(Snowflake guildId, Snowflake emojiId, ModifyGuildEmojiParams args, string reason = null)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(emojiId, nameof(emojiId));
            Preconditions.NotNull(args, nameof(args));
            return _api.ModifyGuildEmojiAsync(guildId, emojiId, args);
        }
        public Task DeleteGuildEmojiAsync(Snowflake guildId, Snowflake emojiId, string reason = null)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(emojiId, nameof(emojiId));
            return _api.DeleteGuildEmojiAsync(guildId, emojiId);
        }

        // Gateway

        public Task<GetGatewayResponse> GetGatewayAsync()
        {
            return _api.GetGatewayAsync();
        }
        public Task<GetBotGatewayResponse> GetBotGatewayAsync()
        {
            return _api.GetBotGatewayAsync();
        }

        // Guild

        public Task<Guild> GetGuildAsync(Snowflake guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.GetGuildAsync(guildId);
        }
        public Task<Guild> CreateGuildAsync(CreateGuildParams args)
        {
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.CreateGuildAsync(args);
        }
        public Task<Guild> ModifyGuildAsync(Snowflake guildId, ModifyGuildParams args, string reason = null)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyGuildAsync(guildId, args);
        }
        public Task DeleteGuildAsync(Snowflake guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.DeleteGuildAsync(guildId);
        }

        public Task<Channel[]> GetGuildChannelsAsync(Snowflake guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.GetGuildChannelsAsync(guildId);
        }
        public Task<Channel> CreateGuildChannelAsync(Snowflake guildId, CreateGuildChannelParams args, string reason = null)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.CreateGuildChannelAsync(guildId, args);
        }
        public Task<Channel[]> ModifyGuildChannelPositionsAsync(Snowflake guildId, ModifyGuildChannelPositionParams[] args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            foreach (var arg in args)
                arg.Validate();
            return _api.ModifyGuildChannelPositionsAsync(guildId, args);
        }

        public Task<GuildMember[]> GetGuildMembersAsync(Snowflake guildId, GetGuildMembersParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.GetGuildMembersAsync(guildId, args);
        }
        public Task<GuildMember> GetGuildMemberAsync(Snowflake guildId, Snowflake userId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(userId, nameof(userId));
            return _api.GetGuildMemberAsync(guildId, userId);
        }
        public Task<GuildMember> AddGuildMemberAsync(Snowflake guildId, Snowflake userId, AddGuildMemberParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(userId, nameof(userId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.AddGuildMemberAsync(guildId, userId, args);
        }
        public Task RemoveGuildMemberAsync(Snowflake guildId, Snowflake userId, string reason = null)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(userId, nameof(userId));
            return _api.RemoveGuildMemberAsync(guildId, userId);
        }
        public Task ModifyGuildMemberAsync(Snowflake guildId, Snowflake userId, ModifyGuildMemberParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(userId, nameof(userId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyGuildMemberAsync(guildId, userId, args);
        }
        public Task ModifyCurrentUserNickAsync(Snowflake guildId, ModifyCurrentUserNickParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyCurrentUserNickAsync(guildId, args);
        }

        public Task AddGuildMemberRoleAsync(Snowflake guildId, Snowflake userId, Snowflake roleId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(userId, nameof(userId));
            Preconditions.NotZero(roleId, nameof(roleId));
            return _api.AddGuildMemberRoleAsync(guildId, userId, roleId);
        }
        public Task RemoveGuildMemberRoleAsync(Snowflake guildId, Snowflake userId, Snowflake roleId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(userId, nameof(userId));
            Preconditions.NotZero(roleId, nameof(roleId));
            return _api.RemoveGuildMemberRoleAsync(guildId, userId, roleId);
        }

        public Task<Ban[]> GetGuildBansAsync(Snowflake guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.GetGuildBansAsync(guildId);
        }
        public Task CreateGuildBanAsync(Snowflake guildId, Snowflake userId, CreateGuildBanParams args, string reason = null)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(userId, nameof(userId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.CreateGuildBanAsync(guildId, userId, args);
        }
        public Task DeleteGuildBanAsync(Snowflake guildId, Snowflake userId, string reason = null)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(userId, nameof(userId));
            return _api.DeleteGuildBanAsync(guildId, userId);
        }

        public Task<Role[]> GetGuildRolesAsync(Snowflake guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.GetGuildRolesAsync(guildId);
        }
        public Task<Role> CreateGuildRoleAsync(Snowflake guildId, CreateGuildRoleParams args, string reason = null)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.CreateGuildRoleAsync(guildId, args);
        }
        public Task<Role> DeleteGuildRoleAsync(Snowflake guildId, Snowflake roleId, string reason = null)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(roleId, nameof(roleId));
            return _api.DeleteGuildRoleAsync(guildId, roleId);
        }
        public Task<Role> ModifyGuildRoleAsync(Snowflake guildId, Snowflake roleId, ModifyGuildRoleParams args, string reason = null)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(roleId, nameof(roleId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyGuildRoleAsync(guildId, roleId, args);
        }
        public Task ModifyGuildRolePositionsAsync(Snowflake guildId, ModifyGuildRolePositionParams[] args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            foreach (var arg in args)
                arg.Validate();
            return _api.ModifyGuildRolePositionsAsync(guildId, args);
        }

        public Task<GuildPruneCountResponse> GetGuildPruneCountAsync(Snowflake guildId, GuildPruneParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.GetGuildPruneCountAsync(guildId, args);
        }
        public Task<GuildPruneCountResponse> PruneGuildMembersAsync(Snowflake guildId, GuildPruneParams args, string reason = null)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.PruneGuildMembersAsync(guildId, args);
        }

        public Task<VoiceRegion[]> GetGuildVoiceRegionsAsync(Snowflake guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.GetGuildVoiceRegionsAsync(guildId);
        }

        public Task<InviteMetadata[]> GetGuildInvitesAsync(Snowflake guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.GetGuildInvitesAsync(guildId);
        }

        public Task<Integration[]> GetGuildIntegrationsAsync(Snowflake guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.GetGuildIntegrationsAsync(guildId);
        }
        public Task<Integration> CreateGuildIntegrationAsync(Snowflake guildId, CreateGuildIntegrationParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.CreateGuildIntegrationAsync(guildId, args);
        }
        public Task DeleteGuildIntegrationAsync(Snowflake guildId, Snowflake integrationId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(integrationId, nameof(integrationId));
            return _api.DeleteGuildIntegrationAsync(guildId, integrationId);
        }
        public Task ModifyGuildIntegrationAsync(Snowflake guildId, Snowflake integrationId, ModifyGuildIntegrationParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(integrationId, nameof(integrationId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyGuildIntegrationAsync(guildId, integrationId, args);
        }
        public Task SyncGuildIntegrationAsync(Snowflake guildId, Snowflake integrationId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(integrationId, nameof(integrationId));
            return _api.SyncGuildIntegrationAsync(guildId, integrationId);
        }

        public Task<GuildEmbed> GetGuildEmbedAsync(Snowflake guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.GetGuildEmbedAsync(guildId);
        }
        public Task<GuildEmbed> ModifyGuildEmbedAsync(Snowflake guildId, ModifyGuildEmbedParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyGuildEmbedAsync(guildId, args);
        }

        public Task<Invite> GetGuildVanityUrlAsync(Snowflake guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.GetGuildVanityUrlAsync(guildId);
        }

        // Invite

        public Task<Invite> GetInviteAsync(Utf8String code, GetInviteParams args)
        {
            Preconditions.NotNullOrWhitespace(code, nameof(code));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.GetInviteAsync(code, args);
        }
        public Task<Invite> DeleteInviteAsync(Utf8String code, string reason = null)
        {
            Preconditions.NotNullOrWhitespace(code, nameof(code));
            return _api.DeleteInviteAsync(code);
        }

        // User

        public Task<User> GetCurrentUserAsync()
        {
            return _api.GetCurrentUserAsync();
        }
        public Task<User> GetUserAsync(Snowflake userId)
        {
            Preconditions.NotZero(userId, nameof(userId));
            return _api.GetUserAsync(userId);
        }
        public Task<User> ModifyCurrentUserAsync(ModifyCurrentUserParams args)
        {
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyCurrentUserAsync(args);
        }

        public Task<UserGuild[]> GetCurrentUserGuildsAsync(GetCurrentUserGuildsParams args)
        {
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.GetCurrentUserGuildsAsync(args);
        }
        public Task LeaveGuildAsync(Snowflake guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.LeaveGuildAsync(guildId);
        }

        public Task<Channel[]> GetDMChannelsAsync()
        {
            return _api.GetDMChannelsAsync();
        }
        public Task<Channel> CreateDMChannelAsync(CreateDMChannelParams args)
        {
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.CreateDMChannelAsync(args);
        }

        public Task<Connection[]> GetUserConnectionsAsync()
        {
            return _api.GetUserConnectionsAsync();
        }

        // Voice

        public Task<VoiceRegion[]> GetVoiceRegionsAsync()
        {
            return _api.GetVoiceRegionsAsync();
        }

        // Webhook

        public Task<Webhook[]> GetChannelWebhooksAsync(Snowflake channelId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            return _api.GetChannelWebhooksAsync(channelId);
        }
        public Task<Webhook[]> GetGuildWebhooksAsync(Snowflake guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.GetGuildWebhooksAsync(guildId);
        }

        public Task<Webhook> GetWebhookAsync(Snowflake webhookId)
        {
            Preconditions.NotZero(webhookId, nameof(webhookId));
            return _api.GetWebhookAsync(webhookId);
        }
        public Task<Webhook> GetWebhookAsync(Snowflake webhookId, Utf8String webhookToken)
        {
            Preconditions.NotZero(webhookId, nameof(webhookId));
            Preconditions.NotNullOrWhitespace(webhookToken, nameof(webhookToken));
            return _api.GetWebhookAsync(webhookId, webhookToken);
        }

        public Task<Webhook> CreateWebhookAsync(Snowflake channelId, CreateWebhookParams args, string reason = null)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.CreateWebhookAsync(channelId, args);
        }

        public Task DeleteWebhookAsync(Snowflake webhookId, string reason = null)
        {
            Preconditions.NotZero(webhookId, nameof(webhookId));
            return _api.DeleteWebhookAsync(webhookId);
        }
        public Task DeleteWebhookAsync(Snowflake webhookId, Utf8String webhookToken, string reason = null)
        {
            Preconditions.NotZero(webhookId, nameof(webhookId));
            Preconditions.NotNullOrWhitespace(webhookToken, nameof(webhookToken));
            return _api.DeleteWebhookAsync(webhookId, webhookToken);
        }

        public Task<Webhook> ModifyWebhookAsync(Snowflake webhookId, ModifyWebhookParams args, string reason = null)
        {
            Preconditions.NotZero(webhookId, nameof(webhookId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyWebhookAsync(webhookId, args);
        }
        public Task<Webhook> ModifyWebhookAsync(Snowflake webhookId, Utf8String webhookToken, ModifyWebhookParams args, string reason = null)
        {
            Preconditions.NotZero(webhookId, nameof(webhookId));
            Preconditions.NotNullOrWhitespace(webhookToken, nameof(webhookToken));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyWebhookAsync(webhookId, webhookToken, args);
        }

        public Task ExecuteWebhookAsync(Snowflake webhookId, Utf8String webhookToken, ExecuteWebhookParams args)
        {
            Preconditions.NotZero(webhookId, nameof(webhookId));
            Preconditions.NotNullOrWhitespace(webhookToken, nameof(webhookToken));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ExecuteWebhookAsync(webhookId, webhookToken, args);
        }

        // OAuth2

        public Task<Application> GetCurrentApplicationAsync()
        {
            return _api.GetCurrentApplicationAsync();
        }
    }
}
