using System;
using System.Collections.Generic;
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

        private static Dictionary<AuditLogChangeKey, Type> TypeSelector { get; } = new Dictionary<AuditLogChangeKey, Type>()
        {
            // General

            [AuditLogChangeKey.Id] = typeof(ulong),
            [AuditLogChangeKey.Type] = typeof(string),

            // Guild

            [AuditLogChangeKey.Name] = typeof(string),
            [AuditLogChangeKey.IconHash] = typeof(string),
            [AuditLogChangeKey.SplashHash] = typeof(string),
            [AuditLogChangeKey.OwnerId] = typeof(ulong),
            [AuditLogChangeKey.Region] = typeof(string),
            [AuditLogChangeKey.AFKChannelId] = typeof(ulong),
            [AuditLogChangeKey.AFKTimeout] = typeof(int),
            [AuditLogChangeKey.MFALevel] = typeof(int),
            [AuditLogChangeKey.VerificationLevel] = typeof(VerificationLevel),
            [AuditLogChangeKey.ExplicitContentFilter] = typeof(ExplicitContentFilter),
            [AuditLogChangeKey.DefaultMessageNotifications] = typeof(DefaultMessageNotifications),
            [AuditLogChangeKey.VanityUrlCode] = typeof(string),
            [AuditLogChangeKey.AddRole] = typeof(Role),
            [AuditLogChangeKey.RemoveRole] = typeof(Role),
            [AuditLogChangeKey.PruneDeleteDays] = typeof(int),
            [AuditLogChangeKey.WidgetEnabled] = typeof(bool),
            [AuditLogChangeKey.WidgetChannelId] = typeof(ulong),

            // Channel

            [AuditLogChangeKey.Position] = typeof(int),
            [AuditLogChangeKey.Topic] = typeof(string),
            [AuditLogChangeKey.Bitrate] = typeof(int),
            [AuditLogChangeKey.PermissionOverwrites] = typeof(Overwrite[]),
            [AuditLogChangeKey.Nsfw] = typeof(bool),
            [AuditLogChangeKey.ApplicationId] = typeof(ulong),

            // Role

            [AuditLogChangeKey.Permissions] = typeof(GuildPermissions),
            [AuditLogChangeKey.Color] = typeof(Color),
            [AuditLogChangeKey.Hoist] = typeof(bool),
            [AuditLogChangeKey.Mentionable] = typeof(bool),
            [AuditLogChangeKey.Allow] = typeof(GuildPermissions),
            [AuditLogChangeKey.Deny] = typeof(GuildPermissions),

            // Invite

            [AuditLogChangeKey.Code] = typeof(string),
            [AuditLogChangeKey.ChannelId] = typeof(ulong),
            [AuditLogChangeKey.InviterId] = typeof(ulong),
            [AuditLogChangeKey.MaxUses] = typeof(int),
            [AuditLogChangeKey.Uses] = typeof(int),
            [AuditLogChangeKey.MaxAge] = typeof(int),
            [AuditLogChangeKey.Temporary] = typeof(bool),

            // User 

            [AuditLogChangeKey.Deaf] = typeof(bool),
            [AuditLogChangeKey.Mute] = typeof(bool),
            [AuditLogChangeKey.Nick] = typeof(string),
            [AuditLogChangeKey.AvatarHash] = typeof(string)
        };
    }
}
