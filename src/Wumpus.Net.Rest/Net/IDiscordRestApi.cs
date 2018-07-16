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
    // TODO: Needs more docs
    [Header("User-Agent", "DiscordBot (https://github.com/RogueException/Wumpus.Net, 1.0.0)")]
    internal interface IDiscordRestApi : IDisposable
    {
        [Header("Authorization")]
        AuthenticationHeaderValue Authorization { get; set; }

        // Audit Log

        /// <summary>
        ///     Returns an <see cref="AuditLog"/> object for the <see cref="Guild"/>. Requires <see cref="GuildPermissions.ViewAuditLog"/>.
        ///     https://discordapp.com/developers/docs/resources/audit-log#get-guild-audit-log
        /// </summary>
        [Get("guilds/{guildId}/audit-logs")]
        Task<AuditLog> GetGuildAuditLogAsync([Path] Snowflake guildId, [QueryMap] GetGuildAuditLogParams args);

        // Channel

        [Get("channels/{channelId}")]
        Task<Channel> GetChannelAsync([Path] Snowflake channelId);
        /// <summary>
        ///     Update a <see cref="Entities.Channel"/>'s settings. Requires the <see cref="Entities.ChannelPermissions.ManageChannels"> permission for the <see cref="Entities.Guild"/>.
        ///     Returns a <see cref="Entities.Channel"/> on success, and a <see cref="System.Net.HttpStatusCode.BadRequest"/> on invalid parameters. Fires a Channel Update Gateway event.
        ///     If modifying a category, individual Channel Update events will fire for each child channel that also changes.
        /// </summary>
        [Put("channels/{channelId}")]
        Task<Channel> ReplaceGuildChannelAsync([Path] Snowflake channelId, [Body] ModifyGuildChannelParams args);
        /// <summary>
        ///     Update a <see cref="Entities.Channel"/>'s settings. Requires the <see cref="Entities.ChannelPermissions.ManageChannels"> permission for the <see cref="Entities.Guild"/>.
        ///     Returns a <see cref="Entities.Channel"/> on success, and a <see cref="System.Net.HttpStatusCode.BadRequest"/> on invalid parameters. Fires a Channel Update Gateway event.
        ///     If modifying a category, individual Channel Update events will fire for each child channel that also changes.
        /// </summary>
        [Put("channels/{channelId}")]
        Task<Channel> ReplaceTextChannelAsync([Path] Snowflake channelId, [Body] ModifyTextChannelParams args);
        /// <summary>
        ///     Update a <see cref="Entities.Channel"/>'s settings. Requires the <see cref="Entities.ChannelPermissions.ManageChannels"> permission for the <see cref="Entities.Guild"/>.
        ///     Returns a <see cref="Entities.Channel"/> on success, and a <see cref="System.Net.HttpStatusCode.BadRequest"/> on invalid parameters. Fires a Channel Update Gateway event.
        ///     If modifying a category, individual Channel Update events will fire for each child channel that also changes.
        /// </summary>
        [Put("channels/{channelId}")]
        Task<Channel> ReplaceVoiceChannelAsync([Path] Snowflake channelId, [Body] ModifyVoiceChannelParams args);
        /// <summary>
        ///     Update a <see cref="Entities.Channel"/>'s settings. Requires the <see cref="Entities.ChannelPermissions.ManageChannels"> permission for the <see cref="Entities.Guild"/>.
        ///     Returns a <see cref="Entities.Channel"/> on success, and a <see cref="System.Net.HttpStatusCode.BadRequest"/> on invalid parameters. Fires a Channel Update Gateway event.
        ///     If modifying a category, individual Channel Update events will fire for each child channel that also changes.
        /// </summary>
        [Patch("channels/{channelId}")]
        Task<Channel> ModifyGuildChannelAsync([Path] Snowflake channelId, [Body] ModifyGuildChannelParams args);
        /// <summary>
        ///     Update a <see cref="Entities.Channel"/>'s settings. Requires the <see cref="Entities.ChannelPermissions.ManageChannels"> permission for the <see cref="Entities.Guild"/>.
        ///     Returns a <see cref="Entities.Channel"/> on success, and a <see cref="System.Net.HttpStatusCode.BadRequest"/> on invalid parameters. Fires a Channel Update Gateway event.
        ///     If modifying a category, individual Channel Update events will fire for each child channel that also changes.
        /// </summary>
        [Patch("channels/{channelId}")]
        Task<Channel> ModifyTextChannelAsync([Path] Snowflake channelId, [Body] ModifyTextChannelParams args);
        /// <summary>
        ///     Update a <see cref="Entities.Channel"/>'s settings. Requires the <see cref="Entities.ChannelPermissions.ManageChannels"> permission for the <see cref="Entities.Guild"/>.
        ///     Returns a <see cref="Entities.Channel"/> on success, and a <see cref="System.Net.HttpStatusCode.BadRequest"/> on invalid parameters. Fires a Channel Update Gateway event.
        ///     If modifying a category, individual Channel Update events will fire for each child channel that also changes.
        /// </summary>
        [Patch("channels/{channelId}")]
        Task<Channel> ModifyVoiceChannelAsync([Path] Snowflake channelId, [Body] ModifyVoiceChannelParams args);
        [Delete("channels/{channelId}")]
        Task<Channel> DeleteChannelAsync([Path] Snowflake channelId);

        /// <summary>
        ///     Returns the <see cref="Entities.Message"/> for a <see cref="Entities.Channel"/>. If operating on a <see cref="Entities.Guild"/> <see cref="Entities.Channel"/>, this endpoint requires the <see cref="Entities.ChannelPermissions.ViewChannel"/> to be present on the current <see cref="Entities.User"/>.
        ///     If the current <see cref="Entities.User"/> is missing <see cref="Entities.ChannelPermissions.ReadMessageHistory"> in the <see cref="Entities.Channel"/> then this will return no messages (since they cannot read the message history).
        ///     Returns an array of <see cref="Entities.Message"/> objects on success.
        ///     https://discordapp.com/developers/docs/resources/channel#get-channel-messages
        /// </summary>
        [Get("channels/{channelId}/messages")]
        Task<List<Message>> GetChannelMessagesAsync([Path] Snowflake channelId, [QueryMap] GetChannelMessagesParams args);
        [Get("channels/{channelId}/messages/{messageId}")]
        Task<Message> GetChannelMessageAsync([Path] Snowflake channelId, [Path] Snowflake messageId);
        /// <summary>
        ///     Post a message to a <see cref="Guild"/> text or DM <see cref="Channel"/>. If operating on a <see cref="Guild"/> <see cref="Channel"/>, this endpoint requires <see cref="ChannelPermissions.SendMessages"/> to be present on the current <see cref="User"/>.
        ///     If the tts field is set to true, <see cref="ChannelPermissions.SendTTSMessages"/> is required for the message to be spoken.
        ///     Returns a <see cref="Message"/> object.
        ///     Fires a Message Create Gateway event.
        /// </summary>
        [Post("channels/{channelId}/messages")]
        Task<Message> CreateMessageAsync([Path] Snowflake channelId, [Body] [QueryMap] CreateMessageParams args);
        [Patch("channels/{channelId}/messages/{messageId}")]
        Task<Message> ModifyMessageAsync([Path] Snowflake channelId, [Path] Snowflake messageId, [Body] ModifyMessageParams args);
        [Delete("channels/{channelId}/messages/{messageId}")]
        Task DeleteMessageAsync([Path] Snowflake channelId, [Path] Snowflake messageId);
        [Post("channels/{channelId}/messages/bulk-delete")]
        Task DeleteMessagesAsync([Path] Snowflake channelId, [Body] DeleteMessagesParams args);

        [Get("channels/{channelId}/messages/{messageId}/reactions/{emoji}")]
        Task<List<User>> GetReactionsAsync([Path] Snowflake channelId, [Path] Snowflake messageId, [Path] Utf8String emoji);
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

        [Post("channels/{channelId}/typing")]
        Task TriggerTypingIndicatorAsync([Path] Snowflake channelId);

        // Emoji

        [Get("guilds/{guildId}/emojis")]
        Task<List<Emoji>> GetGuildEmojisAsync([Path] Snowflake guildId);
        [Get("guilds/{guildId}/emoji/{emojiId}")]
        Task<Emoji> GetGuildEmojiAsync([Path] Snowflake guildId, [Path] Snowflake emojiId);
        [Post("guilds/{guildId}/emojis")]
        Task<Emoji> CreateGuildEmojiAsync([Path] Snowflake guildId, [Body] CreateGuildEmojiParams args);
        [Patch("guilds/{guildId}/emoji/{emojiId}")]
        Task<Emoji> ModifyGuildEmojiAsync([Path] Snowflake guildId, [Path] Snowflake emojiId, [Body] ModifyGuildEmojiParams args);
        [Patch("guilds/{guildId}/emoji/{emojiId}")]
        Task DeleteGuildEmojiAsync([Path] Snowflake guildId, [Path] Snowflake emojiId);

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
        Task<Channel> ModifyGuildChannelPositionsAsync([Path] Snowflake guildId, [Body] IEnumerable<ModifyGuildChannelPositionParams> args);

        [Get("guilds/{guildId}/members")]
        Task<List<GuildMember>> GetGuildMembersAsync([Path] Snowflake guildId, [QueryMap] GetGuildMembersParams args);
        [Get("guilds/{guildId}/members/{userId}")]
        Task<GuildMember> GetGuildMemberAsync([Path] Snowflake guildId, [Path] Snowflake userId);
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
        Task<List<Ban>> GetGuildBansAsync([Path] Snowflake guildId);
        [Put("guilds/{guildId}/bans/{userId}")]
        Task CreateGuildBanAsync([Path] Snowflake guildId, [Path] Snowflake userId, [QueryMap] CreateGuildBanParams args);
        [Delete("guilds/{guildId}/bans/{userId}")]
        Task DeleteGuildBanAsync([Path] Snowflake guildId, [Path] Snowflake userId);

        [Get("guilds/{guildId}/roles")]
        Task<List<Role>> GetGuildRolesAsync([Path] Snowflake guildId);
        [Post("guilds/{guildId}/roles")]
        Task<Role> CreateGuildRoleAsync([Path] Snowflake guildId, [Body] CreateGuildRoleParams args);
        [Delete("guilds/{guildId}/roles/{roleId}")]
        Task<Role> DeleteGuildRoleAsync([Path] Snowflake guildId, [Path] Snowflake roleId);
        [Patch("guilds/{guildId}/roles/{roleId}")]
        Task<Role> ModifyGuildRoleAsync([Path] Snowflake guildId, [Path] Snowflake roleId, [Body] ModifyGuildRoleParams args);
        [Patch("guilds/{guildId}/roles")]
        Task ModifyGuildRolePositionsAsync([Path] Snowflake guildId, [Body] IEnumerable<ModifyGuildRolePositionParams> args);

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
        Task<Integration> CreateGuildIntegrationAsync([Path] Snowflake guildId, [Body] CreateGuildIntegrationParams args);
        [Delete("guilds/{guildId}/integrations/{integrationId}")]
        Task DeleteGuildIntegrationAsync([Path] Snowflake guildId, [Path] Snowflake integrationId);
        [Patch("guilds/{guildId}/integrations/{integrationId}")]
        Task ModifyGuildIntegrationAsync([Path] Snowflake guildId, [Path] Snowflake integrationId, [Body] ModifyGuildIntegrationParams args);
        [Post("guilds/{guildId}/integrations/{integrationId}/sync")]
        Task SyncGuildIntegrationAsync([Path] Snowflake guildId, [Path] Snowflake integrationId);

        [Get("guilds/{guildId}/embed")]
        Task<GuildEmbed> GetGuildEmbedAsync([Path] Snowflake guildId);
        [Patch("guilds/{guildId}/embed")]
        Task<GuildEmbed> ModifyGuildEmbedAsync([Path] Snowflake guildId, [Body] ModifyGuildEmbedParams args);

        [Get("guilds/{guildId}/vanity-url")]
        Task<Invite> GetGuildVanityUrlAsync([Path] Snowflake guildId);

        // Invite

        [Get("invites/{code}")]
        Task<Invite> GetInviteAsync([Path] Utf8String code, [QueryMap] GetInviteParams args);
        [Delete("invites/{code}")]
        Task<Invite> DeleteInviteAsync([Path] Utf8String code);

        // OAuth

        [Get("oauth2/applications/me")]
        Task<Application> GetCurrentApplicationAsync();

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
        Task<List<Channel>> GetDMChannelsAsync();
        [Post("users/@me/channels")]
        Task<Channel> CreateDMChannelAsync([Body] CreateDMChannelParams args);
        [Post("users/@me/channels")]
        Task<Channel> CreateGroupDMChannelAsync([Body] CreateGroupDMChannelParams args);

        [Get("users/@me/connections")]
        Task<List<Connection>> GetUserConnectionsAsync();

        // Voice

        [Get("voice/regions")]
        Task<List<VoiceRegion>> GetVoiceRegionsAsync();

        // Webhook

        [Get("channels/{channelId}/webhooks")]
        Task<List<Webhook>> GetChannelWebhooksAsync([Path] Snowflake channelId);
        [Get("guilds/{guildId}/webhooks")]
        Task<List<Webhook>> GetGuildWebhooksAsync([Path] Snowflake guildId);

        [Get("webhooks/{webhookId}")]
        Task<Webhook> GetWebhookAsync([Path] Snowflake webhookId);
        [Get("webhooks/{webhookId}/{webhookToken}")]
        [Header("Authorization", null)]
        Task<Webhook> GetWebhookAsync([Path] Snowflake webhookId, [Path] Utf8String webhookToken);

        [Post("channels/{channelId}/webhooks")]
        Task<Webhook> CreateWebhookAsync([Path] Snowflake channelId, [Body] CreateWebhookParams args);

        [Delete("webhooks/{webhookId}")]
        Task DeleteWebhookAsync([Path] Snowflake webhookId);
        [Delete("webhooks/{webhookId}/{webhookToken}")]
        [Header("Authorization", null)]
        Task DeleteWebhookAsync([Path] Snowflake webhookId, [Path] Utf8String webhookToken);

        [Patch("webhooks/{webhookId}")]
        Task<Webhook> ModifyWebhookAsync([Path] Snowflake webhookId, ModifyWebhookParams args);
        [Patch("webhooks/{webhookId}/{webhookToken}")]
        [Header("Authorization", null)]
        Task<Webhook> ModifyWebhookAsync([Path] Snowflake webhookId, [Path] Utf8String webhookToken, ModifyWebhookParams args);

        [Post("webhooks/{webhookId}/{webhookToken}")]
        [Header("Authorization", null)]
        Task ExecuteWebhookAsync([Path] Snowflake webhookId, [Path] Utf8String webhookToken, [QueryMap] [Body] ExecuteWebhookParams args);
    }
}
