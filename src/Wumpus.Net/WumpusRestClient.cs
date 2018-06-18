using RestEase;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Voltaic.Serialization.Json;
using Wumpus.Entities;
using Wumpus.Net;
using Wumpus.Requests;
using Wumpus.Responses;

namespace Wumpus
{
    public class WumpusRestClient : IDiscordRestApi, IDisposable
    {
        private readonly IDiscordRestApi _api;
        private readonly JsonSerializer _serializer;

        public AuthenticationHeaderValue Authorization { get => _api.Authorization; set => _api.Authorization = value; }

        public WumpusRestClient(JsonSerializer serializer = null)
        {
            _serializer = serializer ?? new JsonSerializer();
            var httpClient = new HttpClient { BaseAddress = new Uri("https://discordapp.com/api/v6/") };
            _api = RestClient.For<IDiscordRestApi>(new WumpusRequester(httpClient, _serializer));
        }
        public void Dispose() => _api.Dispose();

        // Audit Log

        public Task<AuditLog> GetGuildAuditLogAsync(ulong guildId, GetAuditLogParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.GetGuildAuditLogAsync(guildId, args);
        }

        // Channel

        public Task<Channel> GetChannelAsync(ulong channelId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            return _api.GetChannelAsync(channelId);
        }
        public Task<Channel> ReplaceTextChannelAsync(ulong channelId, ModifyTextChannelParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ReplaceTextChannelAsync(channelId, args);
        }
        public Task<Channel> ReplaceVoiceChannelAsync(ulong channelId, ModifyVoiceChannelParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ReplaceVoiceChannelAsync(channelId, args);
        }
        public Task<Channel> ModifyGuildChannelAsync(ulong channelId, ModifyGuildChannelParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyGuildChannelAsync(channelId, args);
        }
        public Task<Channel> ModifyTextChannelAsync(ulong channelId, ModifyTextChannelParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyTextChannelAsync(channelId, args);
        }
        public Task<Channel> ModifyVoiceChannelAsync(ulong channelId, ModifyVoiceChannelParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyVoiceChannelAsync(channelId, args);
        }
        public Task<Channel> DeleteChannelAsync(ulong channelId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            return _api.DeleteChannelAsync(channelId);
        }

        public Task<List<Message>> GetChannelMessagesAsync(ulong channelId, GetChannelMessagesParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.GetChannelMessagesAsync(channelId, args);
        }
        public Task<Message> GetChannelMessageAsync(ulong channelId, ulong messageId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            return _api.GetChannelMessageAsync(channelId, messageId);
        }
        public Task<Message> CreateMessageAsync(ulong channelId, CreateMessageParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.CreateMessageAsync(channelId, args);
        }
        public Task<Message> ModifyMessageAsync(ulong channelId, ulong messageId, ModifyMessageParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyMessageAsync(channelId, messageId, args);
        }
        public Task DeleteMessageAsync(ulong channelId, ulong messageId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            return _api.DeleteMessageAsync(channelId, messageId);
        }
        public Task DeleteMessagesAsync(ulong channelId, DeleteMessagesParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.DeleteMessagesAsync(channelId, args);
        }

        public Task<List<User>> GetReactionUsersAsync(ulong channelId, ulong messageId, string emoji)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            Preconditions.NotNull(emoji, nameof(emoji));
            return _api.GetReactionUsersAsync(channelId, messageId, emoji);
        }
        public Task CreateReactionAsync(ulong channelId, ulong messageId, string emoji)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            Preconditions.NotNull(emoji, nameof(emoji));
            return _api.CreateReactionAsync(channelId, messageId, emoji);
        }
        public Task DeleteReactionAsync(ulong channelId, ulong messageId, string emoji)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            Preconditions.NotNull(emoji, nameof(emoji));
            return _api.DeleteReactionAsync(channelId, messageId, emoji);
        }
        public Task DeleteReactionAsync(ulong channelId, ulong messageId, ulong userId, string emoji)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            Preconditions.NotZero(userId, nameof(userId));
            Preconditions.NotNull(emoji, nameof(emoji));
            return _api.DeleteReactionAsync(channelId, messageId, userId, emoji);
        }
        public Task DeleteAllReactionsAsync(ulong channelId, ulong messageId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            return _api.DeleteAllReactionsAsync(channelId, messageId);
        }

        public Task EditChannelPermissionsAsync(ulong channelId, ulong overwriteId, ModifyChannelPermissionsParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(overwriteId, nameof(overwriteId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.EditChannelPermissionsAsync(channelId, overwriteId, args);
        }
        public Task DeleteChannelPermissionsAsync(ulong channelId, ulong overwriteId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(overwriteId, nameof(overwriteId));
            return _api.DeleteChannelPermissionsAsync(channelId, overwriteId);
        }

        public Task<List<Invite>> GetChannelInvitesAsync(ulong channelId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            return _api.GetChannelInvitesAsync(channelId);
        }
        public Task<Invite> CreateChannelInviteAsync(ulong channelId, CreateChannelInviteParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.CreateChannelInviteAsync(channelId, args);
        }

        public Task<List<Message>> GetPinnedMessagesAsync(ulong channelId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            return _api.GetPinnedMessagesAsync(channelId);
        }
        public Task PinMessageAsync(ulong channelId, ulong messageId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            return _api.PinMessageAsync(channelId, messageId);
        }
        public Task UnpinMessageAsync(ulong channelId, ulong messageId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(messageId, nameof(messageId));
            return _api.UnpinMessageAsync(channelId, messageId);
        }

        public Task AddRecipientAsync(ulong channelId, ulong userId, AddChannelRecipientParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(userId, nameof(userId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.AddRecipientAsync(channelId, userId, args);
        }
        public Task RemoveRecipientAsync(ulong channelId, ulong userId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotZero(userId, nameof(userId));
            return _api.RemoveRecipientAsync(channelId, userId);
        }

        public Task<Webhook> CreateWebhookAsync(ulong channelId, CreateWebhookParams args)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.CreateWebhookAsync(channelId, args);
        }
        public Task<List<Webhook>> GetChannelWebhooksAsync(ulong channelId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            return _api.GetChannelWebhooksAsync(channelId);
        }

        public Task TriggerTypingIndicatorAsync(ulong channelId)
        {
            Preconditions.NotZero(channelId, nameof(channelId));
            return _api.TriggerTypingIndicatorAsync(channelId);
        }

        // Guild

        public Task<Guild> GetGuildAsync(ulong guildId)
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
        public Task<Guild> ModifyGuildAsync(ulong guildId, ModifyGuildParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyGuildAsync(guildId, args);
        }
        public Task DeleteGuildAsync(ulong guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.DeleteGuildAsync(guildId);
        }

        public Task<List<Channel>> GetGuildChannelsAsync(ulong guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.GetGuildChannelsAsync(guildId);
        }
        public Task<Channel> CreateTextChannelAsync(ulong guildId, CreateTextChannelParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.CreateTextChannelAsync(guildId, args);
        }
        public Task<Channel> CreateVoiceChannelAsync(ulong guildId, CreateVoiceChannelParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.CreateVoiceChannelAsync(guildId, args);
        }
        public Task<Channel> ReorderGuildChannelsAsync(ulong guildId, ModifyGuildChannelPositionParams[] args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            for (int i = 0; i < args.Length; i++)
                args[i].Validate();
            return _api.ReorderGuildChannelsAsync(guildId, args);
        }

        public Task<List<GuildMember>> GetGuildMembersAsync(ulong guildId, GetGuildMembersParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.GetGuildMembersAsync(guildId, args);
        }
        public Task<List<GuildMember>> GetGuildMemberAsync(ulong guildId, ulong userId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(userId, nameof(userId));
            return _api.GetGuildMemberAsync(guildId, userId);
        }
        public Task<GuildMember> AddGuildMemberAsync(ulong guildId, ulong userId, AddGuildMemberParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(userId, nameof(userId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.AddGuildMemberAsync(guildId, userId, args);
        }
        public Task RemoveGuildMemberAsync(ulong guildId, ulong userId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(userId, nameof(userId));
            return _api.RemoveGuildMemberAsync(guildId, userId);
        }
        public Task ModifyGuildMemberAsync(ulong guildId, ulong userId, ModifyGuildMemberParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(userId, nameof(userId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyGuildMemberAsync(guildId, userId, args);
        }
        public Task ModifyCurrentUserNickAsync(ulong guildId, ModifyCurrentUserNickParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyCurrentUserNickAsync(guildId, args);
        }

        public Task AddGuildMemberRoleAsync(ulong guildId, ulong userId, ulong roleId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(userId, nameof(userId));
            Preconditions.NotZero(roleId, nameof(roleId));
            return _api.AddGuildMemberRoleAsync(guildId, userId, roleId);
        }
        public Task RemoveGuildMemberRoleAsync(ulong guildId, ulong userId, ulong roleId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(userId, nameof(userId));
            Preconditions.NotZero(roleId, nameof(roleId));
            return _api.RemoveGuildMemberRoleAsync(guildId, userId, roleId);
        }

        public Task<Ban> GetGuildBansAsync(ulong guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.GetGuildBansAsync(guildId);
        }
        public Task AddGuildBansAsync(ulong guildId, ulong userId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(userId, nameof(userId));
            return _api.AddGuildBansAsync(guildId, userId);
        }
        public Task RemoveGuildBansAsync(ulong guildId, ulong userId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(userId, nameof(userId));
            return _api.RemoveGuildBansAsync(guildId, userId);
        }

        public Task<List<Role>> GetGuildRolesAsync(ulong guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.GetGuildRolesAsync(guildId);
        }
        public Task<Role> CreateGuildRoleAsync(ulong guildId, CreateGuildRoleParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.CreateGuildRoleAsync(guildId, args);
        }
        public Task<Role> DeleteGuildRoleAsync(ulong guildId, ulong roleId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(roleId, nameof(roleId));
            return _api.DeleteGuildRoleAsync(guildId, roleId);
        }
        public Task<Role> ModifyGuildRoleAsync(ulong guildId, ulong roleId, ModifyGuildRoleParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(roleId, nameof(roleId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyGuildRoleAsync(guildId, roleId, args);
        }
        public Task ReorderGuildRolesAsync(ulong guildId, ModifyGuildRolePositionParams[] args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            for (int i = 0; i < args.Length; i++)
                args[i].Validate();
            return _api.ReorderGuildRolesAsync(guildId, args);
        }

        public Task<GuildPruneCountResponse> GetGuildPruneCountAsync(ulong guildId, GuildPruneParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.GetGuildPruneCountAsync(guildId, args);
        }
        public Task<GuildPruneCountResponse> PruneGuildMembersAsync(ulong guildId, GuildPruneParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.PruneGuildMembersAsync(guildId, args);
        }

        public Task<List<VoiceRegion>> GetGuildVoiceRegionsAsync(ulong guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.GetGuildVoiceRegionsAsync(guildId);
        }

        public Task<List<InviteMetadata>> GetGuildInvitesAsync(ulong guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.GetGuildInvitesAsync(guildId);
        }

        public Task<List<Integration>> GetGuildIntegrationsAsync(ulong guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.GetGuildIntegrationsAsync(guildId);
        }
        public Task<Integration> CreateGuildIntegrationsAsync(ulong guildId, CreateGuildIntegrationParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.CreateGuildIntegrationsAsync(guildId, args);
        }
        public Task DeleteGuildIntegrationsAsync(ulong guildId, ulong integrationId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(integrationId, nameof(integrationId));
            return _api.DeleteGuildIntegrationsAsync(guildId, integrationId);
        }
        public Task ModifyGuildIntegrationsAsync(ulong guildId, ulong integrationId, ModifyGuildIntegrationParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(integrationId, nameof(integrationId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyGuildIntegrationsAsync(guildId, integrationId, args);
        }
        public Task SyncGuildIntegrationsAsync(ulong guildId, ulong integrationId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotZero(integrationId, nameof(integrationId));
            return _api.SyncGuildIntegrationsAsync(guildId, integrationId);
        }

        public Task<GuildEmbed> GetGuildEmbedAsync(ulong guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.GetGuildEmbedAsync(guildId);
        }
        public Task<GuildEmbed> ModifyGuildEmbedAsync(ulong guildId, ModifyGuildEmbedParams args)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyGuildEmbedAsync(guildId, args);
        }
        public Task<List<Webhook>> GetGuildWebhooksAsync(ulong guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.GetGuildWebhooksAsync(guildId);
        }

        // Invite

        public Task<Invite> GetInviteAsync(string code)
        {
            Preconditions.NotNullOrWhitespace(code, nameof(code));
            return _api.GetInviteAsync(code);
        }
        public Task<Invite> DeleteInviteAsync(string code)
        {
            Preconditions.NotNullOrWhitespace(code, nameof(code));
            return _api.DeleteInviteAsync(code);
        }
        public Task<Invite> AcceptInviteAsync(string code)
        {
            Preconditions.NotNullOrWhitespace(code, nameof(code));
            return _api.AcceptInviteAsync(code);
        }

        // User

        public Task<User> GetCurrentUserAsync()
        {
            return _api.GetCurrentUserAsync();
        }
        public Task<User> GetUserAsync(ulong userId)
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

        public Task<List<UserGuild>> GetCurrentUserGuildsAsync(GetCurrentUserGuildsParams args)
        {
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.GetCurrentUserGuildsAsync(args);
        }
        public Task LeaveGuildAsync(ulong guildId)
        {
            Preconditions.NotZero(guildId, nameof(guildId));
            return _api.LeaveGuildAsync(guildId);
        }

        public Task<List<Channel>> GetPrivateChannelsAsync()
        {
            return _api.GetPrivateChannelsAsync();
        }
        public Task<Channel> CreateDMChannelAsync(CreateDMChannelParams args)
        {
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.CreateDMChannelAsync(args);
        }
        public Task<Channel> CreateGroupChannelAsync(CreateGroupChannelParams args)
        {
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.CreateGroupChannelAsync(args);
        }

        public Task<List<Connection>> GetUserConnectionsAsync()
        {
            return _api.GetUserConnectionsAsync();
        }

        // Voice

        public Task<List<VoiceRegion>> GetVoiceRegionsAsync()
        {
            return _api.GetVoiceRegionsAsync();
        }

        // Webhook

        public Task<Webhook> GetWebhookAsync(ulong webhookId)
        {
            Preconditions.NotZero(webhookId, nameof(webhookId));
            return _api.GetWebhookAsync(webhookId);
        }
        public Task DeleteWebhookAsync(ulong webhookId)
        {
            Preconditions.NotZero(webhookId, nameof(webhookId));
            return _api.DeleteWebhookAsync(webhookId);
        }
        public Task<Webhook> ModifyWebhookAsync(ulong webhookId, ModifyWebhookParams args)
        {
            Preconditions.NotZero(webhookId, nameof(webhookId));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyWebhookAsync(webhookId, args);
        }

        // Webhook (Anonymous)

        public Task<Webhook> GetWebhookWithTokenAsync(ulong webhookId, string webhookToken)
        {
            Preconditions.NotZero(webhookId, nameof(webhookId));
            Preconditions.NotNullOrWhitespace(webhookToken, nameof(webhookToken));
            return _api.GetWebhookWithTokenAsync(webhookId, webhookToken);
        }
        public Task DeleteWebhookAsync(ulong webhookId, string webhookToken)
        {
            Preconditions.NotZero(webhookId, nameof(webhookId));
            Preconditions.NotNullOrWhitespace(webhookToken, nameof(webhookToken));
            return _api.DeleteWebhookAsync(webhookId, webhookToken);
        }
        public Task<Webhook> ModifyWebhookAsync(ulong webhookId, string webhookToken, ModifyWebhookParams args)
        {
            Preconditions.NotZero(webhookId, nameof(webhookId));
            Preconditions.NotNullOrWhitespace(webhookToken, nameof(webhookToken));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ModifyWebhookAsync(webhookId, webhookToken, args);
        }
        public Task ExecuteWebhookAsync(ulong webhookId, string webhookToken, ExecuteWebhookParams args)
        {
            Preconditions.NotZero(webhookId, nameof(webhookId));
            Preconditions.NotNullOrWhitespace(webhookToken, nameof(webhookToken));
            Preconditions.NotNull(args, nameof(args));
            args.Validate();
            return _api.ExecuteWebhookAsync(webhookId, webhookToken, args);
        }
    }
}
