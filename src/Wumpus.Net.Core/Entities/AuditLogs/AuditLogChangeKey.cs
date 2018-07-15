using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/audit-log#audit-log-change-object-audit-log-change-key </summary>
    [ModelStringEnum]
    public enum AuditLogChangeKey
    {
        // General

        /// <summary> The id of the changed entity - sometimes used in conjunction with other keys. </summary>
        [ModelEnumValue("id")] Id,
        /// <summary> Type of entity created. </summary>
        [ModelEnumValue("type")] Type,

        // Guild

        /// <summary> Name changed. </summary> 
        [ModelEnumValue("name")] Name,
        /// <summary> Icon changed. </summary>
        [ModelEnumValue("icon_hash")] IconHash,
        /// <summary> Invite splash page artwork changed. </summary>
        [ModelEnumValue("splash_hash")] SplashHash,
        /// <summary> Owner changed. </summary>
        [ModelEnumValue("owner_id")] OwnerId,
        /// <summary> <see cref="VoiceRegion"/> changed. </summary>
        [ModelEnumValue("region")] Region,
        /// <summary> AFK <see cref="Channel"/> changed.  </summary>
        [ModelEnumValue("afk_channel_id")] AFKChannelId,
        /// <summary> AFK timeout duration changed.  </summary>
        [ModelEnumValue("afk_timeout")] AFKTimeout,
        /// <summary> <see cref="MfaLevel"/> auth requirement changed. </summary>
        [ModelEnumValue("mfa_level")] MFALevel,
        /// <summary> Required <see cref="Entities.VerificationLevel"/> changed. </summary>
        [ModelEnumValue("verification_level")] VerificationLevel,
        /// <summary> Change in <see cref="Entities.ExplicitContentFilter"/>, whose <see cref="Message"/>s are scanned and delete for explicit content in the <see cref="Guild"/>. </summary>
        [ModelEnumValue("explicit_content_filter")] ExplicitContentFilter,
        /// <summary> Default <see cref="Entities.DefaultMessageNotifications"/> changed. </summary>
        [ModelEnumValue("default_message_notifications")] DefaultMessageNotifications,
        /// <summary> <see cref="Invite"/> vanity url changed. </summary>
        [ModelEnumValue("vanity_url_code")] VanityUrlCode,
        /// <summary> New <see cref="Role"/> added. </summary>
        [ModelEnumValue("$add")] AddRole,
        /// <summary> <see cref="Role"/> removed. </summary>
        [ModelEnumValue("$remove")] RemoveRole,
        /// <summary> Change in number of days after which inactive and <see cref="Role"/>-unassigned <see cref="GuildMember"/>s are kicked. </summary>
        [ModelEnumValue("prune_delete_days")] PruneDeleteDays,
        /// <summary> <see cref="Guild"/> widget enable/disable. </summary>
        [ModelEnumValue("widget_enabled")] WidgetEnabled,
        /// <summary> <see cref="Channel"/> Id of <see cref="Guild"/> widget changed. </summary>
        [ModelEnumValue("widget_channel_id")] WidgetChannelId,

        // Channel

        /// <summary> <see cref="Channel"/> position changed. </summary>
        [ModelEnumValue("position")] Position,
        /// <summary> Text <see cref="Channel"/> topic changed. </summary>
        [ModelEnumValue("topic")] Topic,
        /// <summary> Voice <see cref="Channel"/> bitrate changed. </summary>
        [ModelEnumValue("bitrate")] Bitrate,
        /// <summary> <see cref="Overwrite"/>s on a channel changed.  </summary>
        [ModelEnumValue("permission_overwrites")] PermissionOverwrites,
        /// <summary> <see cref="Channel"/> NSFW restriction changed. </summary>
        [ModelEnumValue("nsfw")] Nsfw,
        /// <summary> <see cref="Application" /> Id of the added or removed <see cref="Webhook"/> or bot. </summary>
        [ModelEnumValue("application_id")] ApplicationId,

        // Role

        /// <summary> <see cref="GuildPermissions"/> or <see cref="ChannelPermissions"/> for a <see cref="Role"/> changed. </summary>
        [ModelEnumValue("permissions")] Permissions,
        /// <summary> <see cref="Role"/> color changed. </summary>
        [ModelEnumValue("color")] Color,
        /// <summary> <see cref="Role"/> is now displayed/no longer displayed separate from other online <see cref="GuildMember"/>s. </summary>
        [ModelEnumValue("hoist")] Hoist,
        /// <summary> <see cref="Role"/> is now mentionable/unmentionable. </summary>
        [ModelEnumValue("mentionable")] Mentionable,
        /// <summary> A <see cref="ChannelPermissions"/> on a <see cref="Channel"/> was allowed for a <see cref="Role"/>. </summary>
        [ModelEnumValue("allow")] Allow,
        /// <summary> A <see cref="ChannelPermissions"/> on a <see cref="Channel"/> was denied for a <see cref="Role"/>. </summary>
        [ModelEnumValue("deny")] Deny,

        // Invite

        /// <summary> <see cref="Invite"/> code changed. </summary>
        [ModelEnumValue("code")] Code,
        /// <summary> <see cref="Channel"/> for <see cref="Invite"/> code changed. </summary>
        [ModelEnumValue("channel_id")] ChannelId,
        /// <summary> <see cref="GuildMember"/> who created <see cref="Invite"/> code changed. </summary>
        [ModelEnumValue("inviter_id")] InviterId,
        /// <summary> Change to the max number of times <see cref="Invite"/> code can be used. </summary>
        [ModelEnumValue("max_uses")] MaxUses,
        /// <summary> Number of times <see cref="Invite"/> code used changed. </summary>
        [ModelEnumValue("uses")] Uses,
        /// <summary> How long <see cref="Invite"/> code lasts changed. </summary>
        [ModelEnumValue("max_age")] MaxAge,
        /// <summary> <see cref="Invite"/> code is temporary/never expires. </summary>
        [ModelEnumValue("temporary")] Temporary,

        // User 

        /// <summary> <see cref="GuildMember"/> server deafened/undeafened. </summary>
        [ModelEnumValue("deaf")] Deaf,
        /// <summary> <see cref="GuildMember"/> server muted/unmuted. </summary>
        [ModelEnumValue("mute")] Mute,
        /// <summary> <see cref="GuildMember"/> nickname changed. </summary>
        [ModelEnumValue("nick")] Nick,
        /// <summary> <see cref="GuildMember"/> avatar changed. </summary>
        [ModelEnumValue("avatar_hash")] AvatarHash
    }
}
