using System;
using System.Reflection;
using Wumpus.Entities;
using Wumpus.Events;
using Wumpus.Requests;
using Wumpus.Serialization.Json.Converters;

namespace Wumpus.Serialization.Json
{
    public class DiscordJsonSerializer : DefaultJsonSerializer
    {
        private static readonly Lazy<DiscordJsonSerializer> _singleton = new Lazy<DiscordJsonSerializer>();
        public static new DiscordJsonSerializer Global => _singleton.Value;

        public DiscordJsonSerializer()
            : this((JsonSerializer)null) { }
        public DiscordJsonSerializer(JsonSerializer parent)
            : base(parent ?? DefaultJsonSerializer.Global)
        {
            AddConverter<Image, ImagePropertyConverter>();
            AddConverter<long, Int53PropertyConverter>((type, prop) => prop?.GetCustomAttribute<Int53Attribute>() != null);
            AddConverter<ulong, UInt53PropertyConverter>((type, prop) => prop?.GetCustomAttribute<Int53Attribute>() != null);

            AddGenericConverter(typeof(EntityOrId<>), typeof(EntityOrIdPropertyConverter<>));
            AddGenericConverter(typeof(Optional<>), typeof(OptionalPropertyConverter<>));

            AddGatewayFrameConverters();
            AddGatewayDispatchFrameConverters();
            AddAuditLogChangeConverter();
        }

        private DiscordJsonSerializer(DiscordJsonSerializer parent)
            : base(parent) { }
        public new DiscordJsonSerializer CreateScope() => new DiscordJsonSerializer(this);

        private void AddGatewayFrameConverters()
        {
            AddSelectorConverter<GatewayOpCode, ObjectPropertyConverter<HelloEvent>>(
                ModelSelectorGroups.GatewayFrame, GatewayOpCode.Hello);
            AddSelectorConverter<GatewayOpCode, BooleanPropertyConverter>(
                ModelSelectorGroups.GatewayFrame, GatewayOpCode.InvalidSession);

            AddSelectorConverter<GatewayOpCode, Int32PropertyConverter>(
                ModelSelectorGroups.GatewayFrame, GatewayOpCode.Heartbeat);
            AddSelectorConverter<GatewayOpCode, Int32PropertyConverter>(
                ModelSelectorGroups.GatewayFrame, GatewayOpCode.HeartbeatAck);
            AddSelectorConverter<GatewayOpCode, ObjectPropertyConverter<IdentifyParams>>(
                ModelSelectorGroups.GatewayFrame, GatewayOpCode.Identify);
            AddSelectorConverter<GatewayOpCode, ObjectPropertyConverter<ResumeParams>>(
                ModelSelectorGroups.GatewayFrame, GatewayOpCode.Resume);
            AddSelectorConverter<GatewayOpCode, ObjectPropertyConverter<StatusUpdateParams>>(
                ModelSelectorGroups.GatewayFrame, GatewayOpCode.StatusUpdate);
            AddSelectorConverter<GatewayOpCode, ObjectPropertyConverter<RequestMembersParams>>(
                ModelSelectorGroups.GatewayFrame, GatewayOpCode.RequestGuildMembers);
            AddSelectorConverter<GatewayOpCode, ObjectPropertyConverter<VoiceStateUpdateParams>>(
                ModelSelectorGroups.GatewayFrame, GatewayOpCode.VoiceStateUpdate);
        }

        private void AddGatewayDispatchFrameConverters()
        {
            AddSelectorConverter<string, ObjectPropertyConverter<SocketReadyEvent>>(
                ModelSelectorGroups.GatewayDispatchFrame, "READY");
            AddSelectorConverter<string, ObjectPropertyConverter<SocketGuild>>(
                ModelSelectorGroups.GatewayDispatchFrame, "GUILD_CREATE");
            AddSelectorConverter<string, ObjectPropertyConverter<Guild>>(
                ModelSelectorGroups.GatewayDispatchFrame, "GUILD_UPDATE");
            AddSelectorConverter<string, ObjectPropertyConverter<GuildEmojiUpdateEvent>>(
                ModelSelectorGroups.GatewayDispatchFrame, "GUILD_EMOJIS_UPDATE");
            AddSelectorConverter<string, ObjectPropertyConverter<GuildSyncEvent>>(
                ModelSelectorGroups.GatewayDispatchFrame, "GUILD_SYNC");
            AddSelectorConverter<string, ObjectPropertyConverter<SocketGuild>>(
                ModelSelectorGroups.GatewayDispatchFrame, "GUILD_DELETE");
            AddSelectorConverter<string, ObjectPropertyConverter<Channel>>(
                ModelSelectorGroups.GatewayDispatchFrame, "CHANNEL_CREATE");
            AddSelectorConverter<string, ObjectPropertyConverter<Channel>>(
                ModelSelectorGroups.GatewayDispatchFrame, "CHANNEL_UPDATE");
            AddSelectorConverter<string, ObjectPropertyConverter<Channel>>(
                ModelSelectorGroups.GatewayDispatchFrame, "CHANNEL_DELETE");
            AddSelectorConverter<string, ObjectPropertyConverter<GuildMemberAddEvent>>(
                ModelSelectorGroups.GatewayDispatchFrame, "GUILD_MEMBER_ADD");
            AddSelectorConverter<string, ObjectPropertyConverter<GuildMemberUpdateEvent>>(
                ModelSelectorGroups.GatewayDispatchFrame, "GUILD_MEMBER_UPDATE");
            AddSelectorConverter<string, ObjectPropertyConverter<GuildMemberRemoveEvent>>(
                ModelSelectorGroups.GatewayDispatchFrame, "GUILD_MEMBER_REMOVE");
            AddSelectorConverter<string, ObjectPropertyConverter<GuildMembersChunkEvent>>(
                ModelSelectorGroups.GatewayDispatchFrame, "GUILD_MEMBERS_CHUNK");
            AddSelectorConverter<string, ObjectPropertyConverter<RecipientEvent>>(
                ModelSelectorGroups.GatewayDispatchFrame, "CHANNEL_RECIPIENT_ADD");
            AddSelectorConverter<string, ObjectPropertyConverter<RecipientEvent>>(
                ModelSelectorGroups.GatewayDispatchFrame, "CHANNEL_RECIPIENT_REMOVE");
            AddSelectorConverter<string, ObjectPropertyConverter<GuildRoleCreateEvent>>(
                ModelSelectorGroups.GatewayDispatchFrame, "GUILD_ROLE_CREATE");
            AddSelectorConverter<string, ObjectPropertyConverter<GuildRoleUpdateEvent>>(
                ModelSelectorGroups.GatewayDispatchFrame, "GUILD_ROLE_UPDATE");
            AddSelectorConverter<string, ObjectPropertyConverter<GuildRoleDeleteEvent>>(
                ModelSelectorGroups.GatewayDispatchFrame, "GUILD_ROLE_UPDATE");
            AddSelectorConverter<string, ObjectPropertyConverter<GuildBanEvent>>(
                ModelSelectorGroups.GatewayDispatchFrame, "GUILD_BAN_ADD");
            AddSelectorConverter<string, ObjectPropertyConverter<GuildBanEvent>>(
                ModelSelectorGroups.GatewayDispatchFrame, "GUILD_BAN_REMOVE");
            AddSelectorConverter<string, ObjectPropertyConverter<Message>>(
                ModelSelectorGroups.GatewayDispatchFrame, "MESSAGE_CREATE");
            AddSelectorConverter<string, ObjectPropertyConverter<Message>>(
                ModelSelectorGroups.GatewayDispatchFrame, "MESSAGE_UPDATE");
            AddSelectorConverter<string, ObjectPropertyConverter<Message>>(
                ModelSelectorGroups.GatewayDispatchFrame, "MESSAGE_DELETE");
            AddSelectorConverter<string, ObjectPropertyConverter<MessageDeleteBulkEvent>>(
                ModelSelectorGroups.GatewayDispatchFrame, "MESSAGE_DELETE_BULK");
            AddSelectorConverter<string, ObjectPropertyConverter<SocketReaction>>(
                ModelSelectorGroups.GatewayDispatchFrame, "MESSAGE_REACTION_ADD");
            AddSelectorConverter<string, ObjectPropertyConverter<SocketReaction>>(
                ModelSelectorGroups.GatewayDispatchFrame, "MESSAGE_REACTION_REMOVE");
            AddSelectorConverter<string, ObjectPropertyConverter<RemoveAllReactionsEvent>>(
                ModelSelectorGroups.GatewayDispatchFrame, "MESSAGE_REACTION_REMOVE_ALL");
            AddSelectorConverter<string, ObjectPropertyConverter<Presence>>(
                ModelSelectorGroups.GatewayDispatchFrame, "PRESENCE_UPDATE");
            AddSelectorConverter<string, ObjectPropertyConverter<User>>(
                ModelSelectorGroups.GatewayDispatchFrame, "USER_UPDATE");
            AddSelectorConverter<string, ObjectPropertyConverter<TypingStartEvent>>(
                ModelSelectorGroups.GatewayDispatchFrame, "TYPING_START");
            AddSelectorConverter<string, ObjectPropertyConverter<VoiceState>>(
                ModelSelectorGroups.GatewayDispatchFrame, "VOICE_STATE_UPDATE");
            AddSelectorConverter<string, ObjectPropertyConverter<VoiceServerUpdateEvent>>(
                ModelSelectorGroups.GatewayDispatchFrame, "VOICE_SERVER_UPDATE");
        }

        private void AddAuditLogChangeConverter()
        {
            // Guild

            AddSelectorConverter<string, StringPropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "name");
            AddSelectorConverter<string, StringPropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "icon_hash");
            AddSelectorConverter<string, StringPropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "splash_hash");
            AddSelectorConverter<string, UInt64PropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "owner_id");
            AddSelectorConverter<string, StringPropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "region");
            AddSelectorConverter<string, UInt64PropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "afk_channel_id");
            AddSelectorConverter<string, Int32PropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "afk_timeout");
            AddSelectorConverter<string, Int32PropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "mfa_level");
            AddSelectorConverter<string, Int64EnumPropertyConverter<VerificationLevel>>(
                ModelSelectorGroups.AuditLogChange, "verification_level");
            AddSelectorConverter<string, Int64EnumPropertyConverter<ExplicitContentFilter>>(
                ModelSelectorGroups.AuditLogChange, "explicit_content_filter");
            AddSelectorConverter<string, Int64EnumPropertyConverter<DefaultMessageNotifications>>(
                ModelSelectorGroups.AuditLogChange, "default_message_notifications");
            AddSelectorConverter<string, StringPropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "vanity_url_code");
            AddSelectorConverter<string, ObjectPropertyConverter<Role>>(
                ModelSelectorGroups.AuditLogChange, "$add");
            AddSelectorConverter<string, ObjectPropertyConverter<Role>>(
                ModelSelectorGroups.AuditLogChange, "$remove");
            AddSelectorConverter<string, Int32PropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "prune_delete_days");
            AddSelectorConverter<string, BooleanPropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "widget_enabled");
            AddSelectorConverter<string, UInt64PropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "widget_channel_id");

            // Channel

            AddSelectorConverter<string, Int32PropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "position");
            AddSelectorConverter<string, StringPropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "topic");
            AddSelectorConverter<string, Int32PropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "bitrate");
            AddSelectorConverter<string, ArrayPropertyConverter<Overwrite>>(
                ModelSelectorGroups.AuditLogChange, "permission_overwrites");
            AddSelectorConverter<string, BooleanPropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "nsfw");
            AddSelectorConverter<string, UInt64PropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "application_id");

            // Role

            AddSelectorConverter<string, UInt64EnumPropertyConverter<GuildPermissions>>(
                ModelSelectorGroups.AuditLogChange, "permissions");
            AddSelectorConverter<string, ColorPropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "color");
            AddSelectorConverter<string, BooleanPropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "hoist");
            AddSelectorConverter<string, BooleanPropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "mentionable");
            AddSelectorConverter<string, UInt64EnumPropertyConverter<GuildPermissions>>(
                ModelSelectorGroups.AuditLogChange, "allow");
            AddSelectorConverter<string, UInt64EnumPropertyConverter<GuildPermissions>>(
                ModelSelectorGroups.AuditLogChange, "deny");

            // Invite

            AddSelectorConverter<string, StringPropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "code");
            AddSelectorConverter<string, UInt64PropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "channel_id");
            AddSelectorConverter<string, UInt64PropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "inviter_id");
            AddSelectorConverter<string, Int32PropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "max_uses");
            AddSelectorConverter<string, Int32PropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "uses");
            AddSelectorConverter<string, Int32PropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "max_age");
            AddSelectorConverter<string, BooleanPropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "temporary");

            // User 

            AddSelectorConverter<string, BooleanPropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "deaf");
            AddSelectorConverter<string, BooleanPropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "mute");
            AddSelectorConverter<string, StringPropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "nick");
            AddSelectorConverter<string, StringPropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "avatar_hash");

            // Any

            AddSelectorConverter<string, UInt64PropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "id");
            AddSelectorConverter<string, StringPropertyConverter>(
                ModelSelectorGroups.AuditLogChange, "type");
        }
    }
}
