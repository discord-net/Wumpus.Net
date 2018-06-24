using System;
using System.Collections.Generic;
using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class AuditLogChange
    {
        /// <summary> xxx </summary>
        [ModelProperty("new_value")]
        [ModelTypeSelector(nameof(Key), nameof(TypeSelector))]
        public object NewValue { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("old_value")]
        [ModelTypeSelector(nameof(Key), nameof(TypeSelector))]
        public object OldValue { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("key")]
        public AuditLogChangeKey Key { get; set; }

        private static Dictionary<AuditLogChangeKey, Type> TypeSelector => new Dictionary<AuditLogChangeKey, Type>()
        {
            // General

            [AuditLogChangeKey.Id] = typeof(Snowflake),
            [AuditLogChangeKey.Type] = typeof(Utf8String),

            // Guild

            [AuditLogChangeKey.Name] = typeof(Utf8String),
            [AuditLogChangeKey.IconHash] = typeof(Utf8String),
            [AuditLogChangeKey.SplashHash] = typeof(Utf8String),
            [AuditLogChangeKey.OwnerId] = typeof(Snowflake),
            [AuditLogChangeKey.Region] = typeof(Utf8String),
            [AuditLogChangeKey.AFKChannelId] = typeof(Snowflake),
            [AuditLogChangeKey.AFKTimeout] = typeof(int),
            [AuditLogChangeKey.MFALevel] = typeof(int),
            [AuditLogChangeKey.VerificationLevel] = typeof(VerificationLevel),
            [AuditLogChangeKey.ExplicitContentFilter] = typeof(ExplicitContentFilter),
            [AuditLogChangeKey.DefaultMessageNotifications] = typeof(DefaultMessageNotifications),
            [AuditLogChangeKey.VanityUrlCode] = typeof(Utf8String),
            [AuditLogChangeKey.AddRole] = typeof(Role),
            [AuditLogChangeKey.RemoveRole] = typeof(Role),
            [AuditLogChangeKey.PruneDeleteDays] = typeof(int),
            [AuditLogChangeKey.WidgetEnabled] = typeof(bool),
            [AuditLogChangeKey.WidgetChannelId] = typeof(Snowflake),

            // Channel

            [AuditLogChangeKey.Position] = typeof(int),
            [AuditLogChangeKey.Topic] = typeof(Utf8String),
            [AuditLogChangeKey.Bitrate] = typeof(int),
            [AuditLogChangeKey.PermissionOverwrites] = typeof(Overwrite[]),
            [AuditLogChangeKey.Nsfw] = typeof(bool),
            [AuditLogChangeKey.ApplicationId] = typeof(Snowflake),

            // Role

            [AuditLogChangeKey.Permissions] = typeof(GuildPermissions),
            [AuditLogChangeKey.Color] = typeof(Color),
            [AuditLogChangeKey.Hoist] = typeof(bool),
            [AuditLogChangeKey.Mentionable] = typeof(bool),
            [AuditLogChangeKey.Allow] = typeof(GuildPermissions),
            [AuditLogChangeKey.Deny] = typeof(GuildPermissions),

            // Invite

            [AuditLogChangeKey.Code] = typeof(Utf8String),
            [AuditLogChangeKey.ChannelId] = typeof(Snowflake),
            [AuditLogChangeKey.InviterId] = typeof(Snowflake),
            [AuditLogChangeKey.MaxUses] = typeof(int),
            [AuditLogChangeKey.Uses] = typeof(int),
            [AuditLogChangeKey.MaxAge] = typeof(int),
            [AuditLogChangeKey.Temporary] = typeof(bool),

            // User 

            [AuditLogChangeKey.Deaf] = typeof(bool),
            [AuditLogChangeKey.Mute] = typeof(bool),
            [AuditLogChangeKey.Nick] = typeof(Utf8String),
            [AuditLogChangeKey.AvatarHash] = typeof(Utf8String)
        };
    }
}
