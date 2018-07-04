using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestEase;
using Voltaic;
using Wumpus.Entities;
using Wumpus.Requests;
using Wumpus.Responses;

namespace Wumpus.Net
{
    [Header("User-Agent", "DiscordBot (https://github.com/RogueException/Wumpus.Net, 1.0.0)")]
    internal interface IDiscordRestApi : IDisposable
    {
        [Header("Authorization")]
        AuthenticationHeaderValue Authorization { get; set; }

        // Audit Log

        [Get("guilds/{guildId}/audit-logs")]
        Task<AuditLog> GetGuildAuditLogAsync([Path] Snowflake guildId, [QueryMap] GetAuditLogParams args);

        // Channel

        [Get("channels/{channelId}")]
        Task<Channel> GetChannelAsync([Path] Snowflake channelId);
        [Put("channels/{channelId}")]
        Task<Channel> ReplaceTextChannelAsync([Path] Snowflake channelId, [Body] ModifyTextChannelParams args);
        [Put("channels/{channelId}")]
        Task<Channel> ReplaceVoiceChannelAsync([Path] Snowflake channelId, [Body] ModifyVoiceChannelParams args);
        [Patch("channels/{channelId}")]
        Task<Channel> ModifyGuildChannelAsync([Path] Snowflake channelId, [Body] ModifyGuildChannelParams args);
        [Patch("channels/{channelId}")]
        Task<Channel> ModifyTextChannelAsync([Path] Snowflake channelId, [Body] ModifyTextChannelParams args);
        [Patch("channels/{channelId}")]
        Task<Channel> ModifyVoiceChannelAsync([Path] Snowflake channelId, [Body] ModifyVoiceChannelParams args);
        [Delete("channels/{channelId}")]
        Task<Channel> DeleteChannelAsync([Path] Snowflake channelId);

        [Get("channels/{channelId}/messages")]
        Task<List<Message>> GetChannelMessagesAsync([Path] Snowflake channelId, [QueryMap] GetChannelMessagesParams args);
        [Get("channels/{channelId}/messages/{messageId}")]
        Task<Message> GetChannelMessageAsync([Path] Snowflake channelId, [Path] Snowflake messageId);
        [Post("channels/{channelId}/messages")]
        Task<Message> CreateMessageAsync([Path] Snowflake channelId, [Body] [QueryMap] CreateMessageParams args);
        [Patch("channels/{channelId}/messages/{messageId}")]
        Task<Message> ModifyMessageAsync([Path] Snowflake channelId, [Path] Snowflake messageId, [Body] ModifyMessageParams args);
        [Delete("channels/{channelId}/messages/{messageId}")]
        Task DeleteMessageAsync([Path] Snowflake channelId, [Path] Snowflake messageId);
        [Post("channels/{channelId}/messages/bulk-delete")]
        Task DeleteMessagesAsync([Path] Snowflake channelId, [Body] DeleteMessagesParams args);

        [Get("channels/{channelId}/messages/{messageId}/reactions/{emoji}")]
        Task<List<User>> GetReactionUsersAsync([Path] Snowflake channelId, [Path] Snowflake messageId, [Path] Utf8String emoji);
        [Put("channels/{channelId}/messages/{messageId}/reactions/{emoji}/@me")]
        Task CreateReactionAsync([Path] Snowflake channelId, [Path] Snowflake messageId, [Path] Utf8String emoji);
        [Delete("channels/{channelId}/messages/{messageId}/reactions/{emoji}/@me")]
        Task DeleteReactionAsync([Path] Snowflake channelId, [Path] Snowflake messageId, [Path] Utf8String emoji);
        [Delete("channels/{channelId}/messages/{messageId}/reactions/{emoji}/{userId}")]
        Task DeleteReactionAsync([Path] Snowflake channelId, [Path] Snowflake messageId, [Path] Snowflake userId, [Path] Utf8String emoji);
        [Delete("channels/{channelId}/messages/{messageId}/reactions")]
        Task DeleteAllReactionsAsync([Path] Snowflake channelId, [Path] Snowflake messageId);

        [Put("channels/{channelId}/permissions/{overwriteId}")]
        Task EditChannelPermissionsAsync([Path] Snowflake channelId, [Path] Snowflake overwriteId, [Body] ModifyChannelPermissionsParams args);
        [Delete("channels/{channelId}/permissions/{overwriteId}")]
        Task DeleteChannelPermissionsAsync([Path] Snowflake channelId, [Path] Snowflake overwriteId);

        [Get("channels/{channelId}/invites")]
        Task<List<Invite>> GetChannelInvitesAsync([Path] Snowflake channelId);
        [Post("channels/{channelId}/invites")]
        Task<Invite> CreateChannelInviteAsync([Path] Snowflake channelId, [Body] CreateChannelInviteParams args);

        [Get("channels/{channelId}/pins")]
        Task<List<Message>> GetPinnedMessagesAsync([Path] Snowflake channelId);
        [Put("channels/{channelId}/pins/{messageId}")]
        Task PinMessageAsync([Path] Snowflake channelId, [Path] Snowflake messageId);
        [Delete("channels/{channelId}/pins/{messageId}")]
        Task UnpinMessageAsync([Path] Snowflake channelId, [Path] Snowflake messageId);

        [Put("channels/{channelId}/recipients/{userId}")]
        Task AddRecipientAsync([Path] Snowflake channelId, [Path] Snowflake userId, [Body] AddChannelRecipientParams args);
        [Delete("channels/{channelId}/recipients/{userId}")]
        Task RemoveRecipientAsync([Path] Snowflake channelId, [Path] Snowflake userId);

        [Post("channels/{channelId}/webhooks")]
        Task<Webhook> CreateWebhookAsync([Path] Snowflake channelId, [Body] CreateWebhookParams args);
        [Get("channels/{channelId}/webhooks")]
        Task<List<Webhook>> GetChannelWebhooksAsync([Path] Snowflake channelId);

        [Post("channels/{channelId}/typing")]
        Task TriggerTypingIndicatorAsync([Path] Snowflake channelId);

        // Gateway

        [Get("gateway")]
        Task<GetGatewayResponse> GetGatewayAsync();
        [Get("gateway/bot")]
        Task<GetBotGatewayResponse> GetBotGatewayAsync();

        // Guild

        [Get("guilds/{guildId}")]
        Task<Guild> GetGuildAsync([Path] Snowflake guildId);
        [Post("guilds")]
        Task<Guild> CreateGuildAsync([Body] CreateGuildParams args);
        [Patch("guilds/{guildId}")]
        Task<Guild> ModifyGuildAsync([Path] Snowflake guildId, [Body] ModifyGuildParams args);
        [Delete("guilds/{guildId}")]
        Task DeleteGuildAsync([Path] Snowflake guildId);

        [Get("guilds/{guildId}/channels")]
        Task<List<Channel>> GetGuildChannelsAsync([Path] Snowflake guildId);
        [Post("guilds/{guildId}/channels")]
        Task<Channel> CreateCategoryChannelAsync([Path] Snowflake guildId, [Body] CreateGuildChannelParams args);
        [Post("guilds/{guildId}/channels")]
        Task<Channel> CreateTextChannelAsync([Path] Snowflake guildId, [Body] CreateTextChannelParams args);
        [Post("guilds/{guildId}/channels")]
        Task<Channel> CreateVoiceChannelAsync([Path] Snowflake guildId, [Body] CreateVoiceChannelParams args);
        [Patch("guilds/{guildId}/channels")]
        Task<Channel> ReorderGuildChannelsAsync([Path] Snowflake guildId, [Body] ModifyGuildChannelPositionParams[] args);

        [Get("guilds/{guildId}/members")]
        Task<List<GuildMember>> GetGuildMembersAsync([Path] Snowflake guildId, [QueryMap] GetGuildMembersParams args);
        [Get("guilds/{guildId}/members/{userId}")]
        Task<List<GuildMember>> GetGuildMemberAsync([Path] Snowflake guildId, [Path] Snowflake userId);
        [Put("guilds/{guildId}/members/{userId}")]
        Task<GuildMember> AddGuildMemberAsync([Path] Snowflake guildId, [Path] Snowflake userId, [Body] AddGuildMemberParams args);
        [Delete("guilds/{guildId}/members/{userId}")]
        Task RemoveGuildMemberAsync([Path] Snowflake guildId, [Path] Snowflake userId);
        [Patch("guilds/{guildId}/members/{userId}")]
        Task ModifyGuildMemberAsync([Path] Snowflake guildId, [Path] Snowflake userId, [Body] ModifyGuildMemberParams args);
        [Patch("guilds/{guildId}/members/@me/nick")]
        Task ModifyCurrentUserNickAsync([Path] Snowflake guildId, [Body] ModifyCurrentUserNickParams args);

        [Put("guilds/{guildId}/members/{userId}/roles/{roleId}")]
        Task AddGuildMemberRoleAsync([Path] Snowflake guildId, [Path] Snowflake userId, [Path] Snowflake roleId);
        [Delete("guilds/{guildId}/members/{userId}/roles/{roleId}")]
        Task RemoveGuildMemberRoleAsync([Path] Snowflake guildId, [Path] Snowflake userId, [Path] Snowflake roleId);

        [Get("guilds/{guildId}/bans")]
        Task<Ban> GetGuildBansAsync([Path] Snowflake guildId);
        [Put("guilds/{guildId}/bans/{userId}")]
        Task AddGuildBansAsync([Path] Snowflake guildId, [Path] Snowflake userId);
        [Delete("guilds/{guildId}/bans/{userId}")]
        Task RemoveGuildBansAsync([Path] Snowflake guildId, [Path] Snowflake userId);

        [Get("guilds/{guildId}/roles")]
        Task<List<Role>> GetGuildRolesAsync([Path] Snowflake guildId);
        [Post("guilds/{guildId}/roles")]
        Task<Role> CreateGuildRoleAsync([Path] Snowflake guildId, [Body] CreateGuildRoleParams args);
        [Delete("guilds/{guildId}/roles/{roleId}")]
        Task<Role> DeleteGuildRoleAsync([Path] Snowflake guildId, [Path] Snowflake roleId);
        [Patch("guilds/{guildId}/roles")]
        Task ReorderGuildRolesAsync([Path] Snowflake guildId, [Body] ModifyGuildRolePositionParams[] args);
        [Patch("guilds/{guildId}/roles/{roleId}")]
        Task<Role> ModifyGuildRoleAsync([Path] Snowflake guildId, [Path] Snowflake roleId, [Body] ModifyGuildRoleParams args);

        [Get("guilds/{guildId}/prune")]
        Task<GuildPruneCountResponse> GetGuildPruneCountAsync([Path] Snowflake guildId, [QueryMap] GuildPruneParams args);
        [Post("guilds/{guildId}/prune")]
        Task<GuildPruneCountResponse> PruneGuildMembersAsync([Path] Snowflake guildId, [QueryMap] GuildPruneParams args);

        [Get("guilds/{guildId}/regions")]
        Task<List<VoiceRegion>> GetGuildVoiceRegionsAsync([Path] Snowflake guildId);

        [Get("guilds/{guildId}/invites")]
        Task<List<InviteMetadata>> GetGuildInvitesAsync([Path] Snowflake guildId);

        [Get("guilds/{guildId}/integrations")]
        Task<List<Integration>> GetGuildIntegrationsAsync([Path] Snowflake guildId);
        [Post("guilds/{guildId}/integrations")]
        Task<Integration> CreateGuildIntegrationsAsync([Path] Snowflake guildId, [Body] CreateGuildIntegrationParams args);
        [Delete("guilds/{guildId}/integrations/{integrationId}")]
        Task DeleteGuildIntegrationsAsync([Path] Snowflake guildId, [Path] Snowflake integrationId);
        [Patch("guilds/{guildId}/integrations/{integrationId}")]
        Task ModifyGuildIntegrationsAsync([Path] Snowflake guildId, [Path] Snowflake integrationId, [Body] ModifyGuildIntegrationParams args);
        [Post("guilds/{guildId}/integrations/{integrationId}/sync")]
        Task SyncGuildIntegrationsAsync([Path] Snowflake guildId, [Path] Snowflake integrationId);

        [Get("guilds/{guildId}/embed")]
        Task<GuildEmbed> GetGuildEmbedAsync([Path] Snowflake guildId);
        [Patch("guilds/{guildId}/embed")]
        Task<GuildEmbed> ModifyGuildEmbedAsync([Path] Snowflake guildId, [Body] ModifyGuildEmbedParams args);

        [Get("guilds/{guildId}/webhooks")]
        Task<List<Webhook>> GetGuildWebhooksAsync([Path] Snowflake guildId);

        [Get("guilds/{guildId}/vanity-url")]
        Task<Invite> GetGuildVanityUrlAsync([Path] Snowflake guildId);

        // Invite

        [Get("invites/{code}")]
        Task<Invite> GetInviteAsync([Path] Utf8String code);
        [Delete("invites/{code}")]
        Task<Invite> DeleteInviteAsync([Path] Utf8String code);

        // User

        [Get("users/@me")]
        Task<User> GetCurrentUserAsync();
        [Get("users/{userId}")]
        Task<User> GetUserAsync([Path] Snowflake userId);
        [Patch("users/@me")]
        Task<User> ModifyCurrentUserAsync([Body] ModifyCurrentUserParams args);

        [Get("users/@me/guilds")]
        Task<List<UserGuild>> GetCurrentUserGuildsAsync([QueryMap] GetCurrentUserGuildsParams args);
        [Delete("users/@me/guilds/{guildId}")]
        Task LeaveGuildAsync([Path] Snowflake guildId);

        [Get("users/@me/channels")]
        Task<List<Channel>> GetPrivateChannelsAsync();
        [Post("users/@me/channels")]
        Task<Channel> CreateDMChannelAsync([Body] CreateDMChannelParams args);
        [Post("users/@me/channels")]
        Task<Channel> CreateGroupChannelAsync([Body] CreateGroupChannelParams args);

        [Get("users/@me/connections")]
        Task<List<Connection>> GetUserConnectionsAsync();

        // Voice

        [Get("voice/regions")]
        Task<List<VoiceRegion>> GetVoiceRegionsAsync();

        // Webhook

        [Get("webhooks/{webhookId}")]
        Task<Webhook> GetWebhookAsync([Path] Snowflake webhookId);
        [Delete("webhooks/{webhookId}")]
        Task DeleteWebhookAsync([Path] Snowflake webhookId);
        [Patch("webhooks/{webhookId}")]
        Task<Webhook> ModifyWebhookAsync([Path] Snowflake webhookId, ModifyWebhookParams args);

        // Webhook (Anonymous)

        [Get("webhooks/{webhookId}/{webhookToken}")]
        [Header("Authorization", null)]
        Task<Webhook> GetWebhookWithTokenAsync([Path] Snowflake webhookId, [Path] Utf8String webhookToken);
        [Delete("webhooks/{webhookId}/{webhookToken}")]
        [Header("Authorization", null)]
        Task DeleteWebhookAsync([Path] Snowflake webhookId, [Path] Utf8String webhookToken);
        [Patch("webhooks/{webhookId}/{webhookToken}")]
        [Header("Authorization", null)]
        Task<Webhook> ModifyWebhookAsync([Path] Snowflake webhookId, [Path] Utf8String webhookToken, ModifyWebhookParams args);
        [Post("webhooks/{webhookId}/{webhookToken}")]
        [Header("Authorization", null)]
        Task ExecuteWebhookAsync([Path] Snowflake webhookId, [Path] Utf8String webhookToken, [QueryMap] [Body] ExecuteWebhookParams args);

        // OAuth

        [Get("/oauth2/applications/me")]
        Task<Application> GetCurrentApplicationAsync();
    }
}
