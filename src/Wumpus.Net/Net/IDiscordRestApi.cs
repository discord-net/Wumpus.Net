using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;
using Wumpus.Entities;
using Wumpus.Requests;
using Wumpus.Responses;
using System;
using System.Net.Http.Headers;

namespace Wumpus
{
    [Header("User-Agent", "DiscordBot (https://github.com/RogueException/Wumpus.Net, 1.0.0)")]
    internal interface IDiscordRestApi : IDisposable
    {
        [Header("Authorization")]
        AuthenticationHeaderValue Authorization { get; set; }

        // Audit Log

        [Get("guilds/{guildId}/audit-logs")]
        Task<AuditLog> GetGuildAuditLogAsync([Path] ulong guildId, [QueryMap] GetAuditLogParams args);

        // Channel

        [Get("channels/{channelId}")]
        Task<Channel> GetChannelAsync([Path] ulong channelId);
        [Put("channels/{channelId}")]
        Task<Channel> ReplaceTextChannelAsync([Path] ulong channelId, [Body] ModifyTextChannelParams args);
        [Put("channels/{channelId}")]
        Task<Channel> ReplaceVoiceChannelAsync([Path] ulong channelId, [Body] ModifyVoiceChannelParams args);
        [Patch("channels/{channelId}")]
        Task<Channel> ModifyGuildChannelAsync([Path] ulong channelId, [Body] ModifyGuildChannelParams args);
        [Patch("channels/{channelId}")]
        Task<Channel> ModifyTextChannelAsync([Path] ulong channelId, [Body] ModifyTextChannelParams args);
        [Patch("channels/{channelId}")]
        Task<Channel> ModifyVoiceChannelAsync([Path] ulong channelId, [Body] ModifyVoiceChannelParams args);
        [Delete("channels/{channelId}")]
        Task<Channel> DeleteChannelAsync([Path] ulong channelId);

        [Get("channels/{channelId}/messages")]
        Task<List<Message>> GetChannelMessagesAsync([Path] ulong channelId, [QueryMap] GetChannelMessagesParams args);
        [Get("channels/{channelId}/messages/{messageId}")]
        Task<Message> GetChannelMessageAsync([Path] ulong channelId, [Path] ulong messageId);
        [Post("channels/{channelId}/messages")]
        Task<Message> CreateMessageAsync([Path] ulong channelId, [Body] [QueryMap] CreateMessageParams args);
        [Patch("channels/{channelId}/messages/{messageId}")]
        Task<Message> ModifyMessageAsync([Path] ulong channelId, [Path] ulong messageId, [Body] ModifyMessageParams args);
        [Delete("channels/{channelId}/messages/{messageId}")]
        Task DeleteMessageAsync([Path] ulong channelId, [Path] ulong messageId);
        [Post("channels/{channelId}/messages/bulk-delete")]
        Task DeleteMessagesAsync([Path] ulong channelId, [Body] DeleteMessagesParams args);

        [Get("channels/{channelId}/messages/{messageId}/reactions/{emoji}")]
        Task<List<User>> GetReactionUsersAsync([Path] ulong channelId, [Path] ulong messageId, [Path] string emoji);
        [Put("channels/{channelId}/messages/{messageId}/reactions/{emoji}/@me")]
        Task CreateReactionAsync([Path] ulong channelId, [Path] ulong messageId, [Path] string emoji);
        [Delete("channels/{channelId}/messages/{messageId}/reactions/{emoji}/@me")]
        Task DeleteReactionAsync([Path] ulong channelId, [Path] ulong messageId, [Path] string emoji);
        [Delete("channels/{channelId}/messages/{messageId}/reactions/{emoji}/{userId}")]
        Task DeleteReactionAsync([Path] ulong channelId, [Path] ulong messageId, [Path] ulong userId, [Path] string emoji);
        [Delete("channels/{channelId}/messages/{messageId}/reactions")]
        Task DeleteAllReactionsAsync([Path] ulong channelId, [Path] ulong messageId);

        [Put("channels/{channelId}/permissions/{overwriteId}")]
        Task EditChannelPermissionsAsync([Path] ulong channelId, [Path] ulong overwriteId, [Body] ModifyChannelPermissionsParams args);
        [Delete("channels/{channelId}/permissions/{overwriteId}")]
        Task DeleteChannelPermissionsAsync([Path] ulong channelId, [Path] ulong overwriteId);

        [Get("channels/{channelId}/invites")]
        Task<List<Invite>> GetChannelInvitesAsync([Path] ulong channelId);
        [Post("channels/{channelId}/invites")]
        Task<Invite> CreateChannelInviteAsync([Path] ulong channelId, [Body] CreateChannelInviteParams args);

        [Get("channels/{channelId}/pins")]
        Task<List<Message>> GetPinnedMessagesAsync([Path] ulong channelId);
        [Put("channels/{channelId}/pins/{messageId}")]
        Task PinMessageAsync([Path] ulong channelId, [Path] ulong messageId);
        [Delete("channels/{channelId}/pins/{messageId}")]
        Task UnpinMessageAsync([Path] ulong channelId, [Path] ulong messageId);

        [Put("channels/{channelId}/recipients/{userId}")]
        Task AddRecipientAsync([Path] ulong channelId, [Path] ulong userId, [Body] AddChannelRecipientParams args);
        [Delete("channels/{channelId}/recipients/{userId}")]
        Task RemoveRecipientAsync([Path] ulong channelId, [Path] ulong userId);

        [Post("channels/{channelId}/webhooks")]
        Task<Webhook> CreateWebhookAsync([Path] ulong channelId, [Body] CreateWebhookParams args);
        [Get("channels/{channelId}/webhooks")]
        Task<List<Webhook>> GetChannelWebhooksAsync([Path] ulong channelId);

        [Post("channels/{channelId}/typing")]
        Task TriggerTypingIndicatorAsync([Path] ulong channelId);

        // Guild

        [Get("guilds/{guildId}")]
        Task<Guild> GetGuildAsync([Path] ulong guildId);
        [Post("guilds")]
        Task<Guild> CreateGuildAsync([Body] CreateGuildParams args);
        [Patch("guilds/{guildId}")]
        Task<Guild> ModifyGuildAsync([Path] ulong guildId, [Body] ModifyGuildParams args);
        [Delete("guilds/{guildId}")]
        Task DeleteGuildAsync([Path] ulong guildId);

        [Get("guilds/{guildId}/channels")]
        Task<List<Channel>> GetGuildChannelsAsync([Path] ulong guildId);
        [Post("guilds/{guildId}/channels")]
        Task<Channel> CreateTextChannelAsync([Path] ulong guildId, [Body] CreateTextChannelParams args);
        [Post("guilds/{guildId}/channels")]
        Task<Channel> CreateVoiceChannelAsync([Path] ulong guildId, [Body] CreateVoiceChannelParams args);
        [Patch("guilds/{guildId}/channels")]
        Task<Channel> ReorderGuildChannelsAsync([Path] ulong guildId, [Body] ModifyGuildChannelPositionParams[] args);

        [Get("guilds/{guildId}/members")]
        Task<List<GuildMember>> GetGuildMembersAsync([Path] ulong guildId, [QueryMap] GetGuildMembersParams args);
        [Get("guilds/{guildId}/members/{userId}")]
        Task<List<GuildMember>> GetGuildMemberAsync([Path] ulong guildId, [Path] ulong userId);
        [Put("guilds/{guildId}/members/{userId}")]
        Task<GuildMember> AddGuildMemberAsync([Path] ulong guildId, [Path] ulong userId, [Body] AddGuildMemberParams args);
        [Delete("guilds/{guildId}/members/{userId}")]
        Task RemoveGuildMemberAsync([Path] ulong guildId, [Path] ulong userId);
        [Patch("guilds/{guildId}/members/{userId}")]
        Task ModifyGuildMemberAsync([Path] ulong guildId, [Path] ulong userId, [Body] ModifyGuildMemberParams args);
        [Patch("guilds/{guildId}/members/@me/nick")]
        Task ModifyCurrentUserNickAsync([Path] ulong guildId, [Body] ModifyCurrentUserNickParams args);

        [Put("guilds/{guildId}/members/{userId}/roles/{roleId}")]
        Task AddGuildMemberRoleAsync([Path] ulong guildId, [Path] ulong userId, [Path] ulong roleId);
        [Delete("guilds/{guildId}/members/{userId}/roles/{roleId}")]
        Task RemoveGuildMemberRoleAsync([Path] ulong guildId, [Path] ulong userId, [Path] ulong roleId);

        [Get("guilds/{guildId}/bans")]
        Task<Ban> GetGuildBansAsync([Path] ulong guildId);
        [Put("guilds/{guildId}/bans/{userId}")]
        Task AddGuildBansAsync([Path] ulong guildId, [Path] ulong userId);
        [Delete("guilds/{guildId}/bans/{userId}")]
        Task RemoveGuildBansAsync([Path] ulong guildId, [Path] ulong userId);

        [Get("guilds/{guildId}/roles")]
        Task<List<Role>> GetGuildRolesAsync([Path] ulong guildId);
        [Post("guilds/{guildId}/roles")]
        Task<Role> CreateGuildRoleAsync([Path] ulong guildId, [Body] CreateGuildRoleParams args);
        [Delete("guilds/{guildId}/roles/{roleId}")]
        Task<Role> DeleteGuildRoleAsync([Path] ulong guildId, [Path] ulong roleId);
        [Patch("guilds/{guildId}/roles")]
        Task ReorderGuildRolesAsync([Path] ulong guildId, [Body] ModifyGuildRolePositionParams[] args);
        [Patch("guilds/{guildId}/roles/{roleId}")]
        Task<Role> ModifyGuildRoleAsync([Path] ulong guildId, [Path] ulong roleId, [Body] ModifyGuildRoleParams args);

        [Get("guilds/{guildId}/prune")]
        Task<GuildPruneCountResponse> GetGuildPruneCountAsync([Path] ulong guildId, [QueryMap] GuildPruneParams args);
        [Post("guilds/{guildId}/prune")]
        Task<GuildPruneCountResponse> PruneGuildMembersAsync([Path] ulong guildId, [QueryMap] GuildPruneParams args);

        [Get("guilds/{guildId}/regions")]
        Task<List<VoiceRegion>> GetGuildVoiceRegionsAsync([Path] ulong guildId);

        [Get("guilds/{guildId}/invites")]
        Task<List<InviteMetadata>> GetGuildInvitesAsync([Path] ulong guildId);

        [Get("guilds/{guildId}/integrations")]
        Task<List<Integration>> GetGuildIntegrationsAsync([Path] ulong guildId);
        [Post("guilds/{guildId}/integrations")]
        Task<Integration> CreateGuildIntegrationsAsync([Path] ulong guildId, [Body] CreateGuildIntegrationParams args);
        [Delete("guilds/{guildId}/integrations/{integrationId}")]
        Task DeleteGuildIntegrationsAsync([Path] ulong guildId, [Path] ulong integrationId);
        [Patch("guilds/{guildId}/integrations/{integrationId}")]
        Task ModifyGuildIntegrationsAsync([Path] ulong guildId, [Path] ulong integrationId, [Body] ModifyGuildIntegrationParams args);
        [Post("guilds/{guildId}/integrations/{integrationId}/sync")]
        Task SyncGuildIntegrationsAsync([Path] ulong guildId, [Path] ulong integrationId);

        [Get("guilds/{guildId}/embed")]
        Task<GuildEmbed> GetGuildEmbedAsync([Path] ulong guildId);
        [Patch("guilds/{guildId}/embed")]
        Task<GuildEmbed> ModifyGuildEmbedAsync([Path] ulong guildId, [Body] ModifyGuildEmbedParams args);

        [Get("guilds/{guildId}/webhooks")]
        Task<List<Webhook>> GetGuildWebhooksAsync([Path] ulong guildId);

        // Invite

        [Get("invites/{code}")]
        Task<Invite> GetInviteAsync([Path] string code);
        [Delete("invites/{code}")]
        Task<Invite> DeleteInviteAsync([Path] string code);
        [Post("invites/{code}")]
        Task<Invite> AcceptInviteAsync([Path] string code);

        // User

        [Get("users/@me")]
        Task<User> GetCurrentUserAsync();
        [Get("users/{userId}")]
        Task<User> GetUserAsync([Path] ulong userId);
        [Patch("users/@me")]
        Task<User> ModifyCurrentUserAsync([Body] ModifyCurrentUserParams args);

        [Get("users/@me/guilds")]
        Task<List<UserGuild>> GetCurrentUserGuildsAsync([QueryMap] GetCurrentUserGuildsParams args);
        [Delete("users/@me/guilds/{guildId}")]
        Task LeaveGuildAsync([Path] ulong guildId);

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
        Task<Webhook> GetWebhookAsync([Path] ulong webhookId);
        [Delete("webhooks/{webhookId}")]
        Task DeleteWebhookAsync([Path] ulong webhookId);
        [Patch("webhooks/{webhookId}")]
        Task<Webhook> ModifyWebhookAsync([Path] ulong webhookId, ModifyWebhookParams args);

        // Webhook (Anonymous)

        [Get("webhooks/{webhookId}/{webhookToken}")]
        [Header("Authorization", null)]
        Task<Webhook> GetWebhookWithTokenAsync([Path] ulong webhookId, [Path] string webhookToken);
        [Delete("webhooks/{webhookId}/{webhookToken}")]
        [Header("Authorization", null)]
        Task DeleteWebhookAsync([Path] ulong webhookId, [Path] string webhookToken);
        [Patch("webhooks/{webhookId}/{webhookToken}")]
        [Header("Authorization", null)]
        Task<Webhook> ModifyWebhookAsync([Path] ulong webhookId, [Path] string webhookToken, ModifyWebhookParams args);
        [Post("webhooks/{webhookId}/{webhookToken}")]
        [Header("Authorization", null)]
        Task ExecuteWebhookAsync([Path] ulong webhookId, [Path] string webhookToken, [QueryMap] [Body] ExecuteWebhookParams args);
    }
}
