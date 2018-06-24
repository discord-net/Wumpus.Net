using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    [ModelStringEnum]
    public enum GatewayDispatchType : byte
    {
        [ModelEnumValue("READY")] Ready,
        [ModelEnumValue("GUILD_CREATE")] GuildCreate,
        [ModelEnumValue("GUILD_UPDATE")] GuildUpdate,
        [ModelEnumValue("GUILD_DELETE")] GuildDelete,
        [ModelEnumValue("GUILD_EMOJIS_UPDATE")] GuildEmojisUpdate,
        [ModelEnumValue("GUILD_SYNC")] GuildSync,
        [ModelEnumValue("CHANNEL_CREATE")] ChannelCreate,
        [ModelEnumValue("CHANNEL_UPDATE")] ChannelUpdate,
        [ModelEnumValue("CHANNEL_DELETE")] ChannelDelete,
        [ModelEnumValue("GUILD_MEMBER_ADD")] GuildMemberAdd,
        [ModelEnumValue("GUILD_MEMBER_UPDATE")] GuildMemberUpdate,
        [ModelEnumValue("GUILD_MEMBER_REMOVE")] GuildMemberRemove,
        [ModelEnumValue("GUILD_MEMBERS_CHUNK")] GuildMembersChunk,
        [ModelEnumValue("CHANNEL_RECIPIENT_ADD")] ChannelRecipientAdd,
        [ModelEnumValue("CHANNEL_RECIPIENT_REMOVE")] ChannelRecipientRemove,
        [ModelEnumValue("GUILD_ROLE_CREATE")] GuildRoleCreate,
        [ModelEnumValue("GUILD_ROLE_UPDATE")] GuildRoleUpdate,
        [ModelEnumValue("GUILD_ROLE_DELETE")] GuildRoleDelete,
        [ModelEnumValue("GUILD_BAN_ADD")] GuildBanAdd,
        [ModelEnumValue("GUILD_BAN_REMOVE")] GuildBanRemove,
        [ModelEnumValue("MESSAGE_CREATE")] MessageCreate,
        [ModelEnumValue("MESSAGE_UPDATE")] MessageUpdate,
        [ModelEnumValue("MESSAGE_DELETE")] MessageDelete,
        [ModelEnumValue("MESSAGE_DELETE_BULK")] MessageDeleteBulk,
        [ModelEnumValue("MESSAGE_REACTION_ADD")] MessageReactionAdd,
        [ModelEnumValue("MESSAGE_REACTION_REMOVE")] MessageReactionRemove,
        [ModelEnumValue("MESSAGE_REACTION_REMOVE_ALL")] MessageReactionRemoveAll,
        [ModelEnumValue("PRESENCE_UPDATE")] PresenceUpdate,
        [ModelEnumValue("USER_UPDATE")] UserUpdate,
        [ModelEnumValue("TYPING_START")] TypingStart,
        [ModelEnumValue("VOICE_STATE_UPDATE")] VoiceStateUpdate,
        [ModelEnumValue("VOICE_SERVER_UPDATE")] VoiceServerUpdate
    }
}
