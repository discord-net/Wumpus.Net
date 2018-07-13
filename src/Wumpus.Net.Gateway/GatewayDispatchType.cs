using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> Events are payloads sent over the socket to a client that correspond events in Discord. </summary>
    [ModelStringEnum]
    public enum GatewayDispatchType : byte
    {
        /// <summary> Contains the initial state information. </summary>
        [ModelEnumValue("READY")] Ready,
        /// <summary> Lazy-load for unavailable <see cref="Entities.Guild"/>, <see cref="Entities.Guild"/> became available, or a <see cref="Entities.User"/> joined a new <see cref="Entities.Guild"/>. </summary>
        [ModelEnumValue("GUILD_CREATE")] GuildCreate,
        /// <summary> <see cref="Entities.Guild"/> was updated. </summary>
        [ModelEnumValue("GUILD_UPDATE")] GuildUpdate,
        /// <summary> <see cref="Entities.Guild"/> became unavailable, or <see cref="Entities.User"/> left/was removed from a <see cref="Entities.Guild"/>. </summary>
        [ModelEnumValue("GUILD_DELETE")] GuildDelete,
        /// <summary> <see cref="Entities.Guild"/> <see cref="Entities.Emoji"/>s were updated. </summary>
        [ModelEnumValue("GUILD_EMOJIS_UPDATE")] GuildEmojisUpdate,
        /// <summary> New <see cref="Entities.Channel"/> created. </summary>
        [ModelEnumValue("CHANNEL_CREATE")] ChannelCreate,
        /// <summary> <see cref="Entities.Channel"/> was updated. </summary>
        [ModelEnumValue("CHANNEL_UPDATE")] ChannelUpdate,
        /// <summary> <see cref="Entities.Channel"/> was deleted. </summary>
        [ModelEnumValue("CHANNEL_DELETE")] ChannelDelete,
        /// <summary> New <see cref="Entities.User"/> joined <see cref="Entities.Guild"/>. </summary>
        [ModelEnumValue("GUILD_MEMBER_ADD")] GuildMemberAdd,
        /// <summary> <see cref="Entities.GuildMember"/> was updated. </summary>
        [ModelEnumValue("GUILD_MEMBER_UPDATE")] GuildMemberUpdate,
        /// <summary> <see cref="Entities.User"/> was removed from a <see cref="Entities.Guild"/>. </summary>
        [ModelEnumValue("GUILD_MEMBER_REMOVE")] GuildMemberRemove,
        /// <summary> Response to <see cref="GatewayOperation.RequestGuildMembers"/>. </summary>
        [ModelEnumValue("GUILD_MEMBERS_CHUNK")] GuildMembersChunk,
        /// <summary> <see cref="Entities.Guild"/> <see cref="Entities.Role"/> was created. </summary>
        [ModelEnumValue("GUILD_ROLE_CREATE")] GuildRoleCreate,
        /// <summary> <see cref="Entities.Guild"/> <see cref="Entities.Role"/> was updated. </summary>
        [ModelEnumValue("GUILD_ROLE_UPDATE")] GuildRoleUpdate,
        /// <summary> <see cref="Entities.Guild"/> <see cref="Entities.Role"/> was deleted. </summary>
        [ModelEnumValue("GUILD_ROLE_DELETE")] GuildRoleDelete,
        /// <summary> <see cref="Entities.User"/> was banned from a <see cref="Entities.Guild"/>. </summary>
        [ModelEnumValue("GUILD_BAN_ADD")] GuildBanAdd,
        /// <summary> <see cref="Entities.User"/> was unbanned from a <see cref="Entities.Guild"/>. </summary>
        [ModelEnumValue("GUILD_BAN_REMOVE")] GuildBanRemove,
        /// <summary> <see cref="Entities.Message"/> was created. </summary>
        [ModelEnumValue("MESSAGE_CREATE")] MessageCreate,
        /// <summary> <see cref="Entities.Message"/> was updated. </summary>
        [ModelEnumValue("MESSAGE_UPDATE")] MessageUpdate,
        /// <summary> <see cref="Entities.Message"/> was deleted. </summary>
        [ModelEnumValue("MESSAGE_DELETE")] MessageDelete,
        /// <summary> Multiple <see cref="Entities.Message"/>s were deleted at once.</summary>
        [ModelEnumValue("MESSAGE_DELETE_BULK")] MessageDeleteBulk,
        /// <summary> <see cref="Entities.User"/> reacted to a <see cref="Entities.Message"/>. </summary>
        [ModelEnumValue("MESSAGE_REACTION_ADD")] MessageReactionAdd,
        /// <summary> <see cref="Entities.User"/> removed a reaction from a <see cref="Entities.Message"/>. </summary>
        [ModelEnumValue("MESSAGE_REACTION_REMOVE")] MessageReactionRemove,
        /// <summary> All <see cref="Entities.Reaction"/>s were explicitly removed from a <see cref="Entities.Message"/>. </summary>
        [ModelEnumValue("MESSAGE_REACTION_REMOVE_ALL")] MessageReactionRemoveAll,
        /// <summary> <see cref="Entities.User"/>'s <see cref="Entities.Presence"/> was updated in a <see cref="Entities.Guild"/>. </summary>
        [ModelEnumValue("PRESENCE_UPDATE")] PresenceUpdate,
        /// <summary> Properties about a <see cref="Entities.User"/> changed. </summary>
        [ModelEnumValue("USER_UPDATE")] UserUpdate,
        /// <summary> <see cref="Entities.User"/> started typing in a <see cref="Entities.Channel"/>. </summary>
        [ModelEnumValue("TYPING_START")] TypingStart,
        /// <summary> Someone joined, left, or moved a <see cref="Entities.Channel"/>. </summary>
        [ModelEnumValue("VOICE_STATE_UPDATE")] VoiceStateUpdate,
        /// <summary> <see cref="Entities.Guild"/>'s voice server was updated. </summary>
        [ModelEnumValue("VOICE_SERVER_UPDATE")] VoiceServerUpdate,
        /// <summary> <see cref="Entities.Guild"/> <see cref="Entities.Channel"/> <see cref="Entities.Webhook"/> was created, updated, or deleted. </summary>
        [ModelEnumValue("WEBHOOKS_UPDATE")] WebhooksUpdate
    }
}
