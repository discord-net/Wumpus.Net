using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    [ModelStringEnum]
    public enum AuditLogChangeKey
    {
        // General

        [ModelEnumValue("id")] Id,
        [ModelEnumValue("type")] Type,

        // Guild

        [ModelEnumValue("name")] Name,
        [ModelEnumValue("icon_hash")] IconHash,
        [ModelEnumValue("splash_hash")] SplashHash,
        [ModelEnumValue("owner_id")] OwnerId,
        [ModelEnumValue("region")] Region,
        [ModelEnumValue("afk_channel_id")] AFKChannelId,
        [ModelEnumValue("afk_timeout")] AFKTimeout,
        [ModelEnumValue("mfa_level")] MFALevel,
        [ModelEnumValue("verification_level")] VerificationLevel,
        [ModelEnumValue("explicit_content_filter")] ExplicitContentFilter,
        [ModelEnumValue("default_message_notifications")] DefaultMessageNotifications,
        [ModelEnumValue("vanity_url_code")] VanityUrlCode,
        [ModelEnumValue("$add")] AddRole,
        [ModelEnumValue("$remove")] RemoveRole,
        [ModelEnumValue("prune_delete_days")] PruneDeleteDays,
        [ModelEnumValue("widget_enabled")] WidgetEnabled,
        [ModelEnumValue("widget_channel_id")] WidgetChannelId,

        // Channel

        [ModelEnumValue("position")] Position,
        [ModelEnumValue("topic")] Topic,
        [ModelEnumValue("bitrate")] Bitrate,
        [ModelEnumValue("permission_overwrites")] PermissionOverwrites,
        [ModelEnumValue("nsfw")] Nsfw,
        [ModelEnumValue("application_id")] ApplicationId,

        // Role

        [ModelEnumValue("permissions")] Permissions,
        [ModelEnumValue("color")] Color,
        [ModelEnumValue("hoist")] Hoist,
        [ModelEnumValue("mentionable")] Mentionable,
        [ModelEnumValue("allow")] Allow,
        [ModelEnumValue("deny")] Deny,

        // Invite

        [ModelEnumValue("code")] Code,
        [ModelEnumValue("channel_id")] ChannelId,
        [ModelEnumValue("inviter_id")] InviterId,
        [ModelEnumValue("max_uses")] MaxUses,
        [ModelEnumValue("uses")] Uses,
        [ModelEnumValue("max_age")] MaxAge,
        [ModelEnumValue("temporary")] Temporary,

        // User 

        [ModelEnumValue("deaf")] Deaf,
        [ModelEnumValue("mute")] Mute,
        [ModelEnumValue("nick")] Nick,
        [ModelEnumValue("avatar_hash")] AvatarHash
    }
}
