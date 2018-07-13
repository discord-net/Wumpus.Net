using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/audit-log#audit-log-object </summary>
    public class AuditLog
    {
        public const int MinimumGetEntryAmount = 1;
        public const int DefaultGetEntryAmount = 50;
        public const int MaximumGetEntryAmount = 100;

        /// <summary> List of <see cref="Webhook"/>s found in the <see cref="AuditLog"/>. </summary>
        [ModelProperty("webhooks")]
        public Webhook[] Webhooks { get; set; }
        /// <summary> List of <see cref="User"/>s found in the <see cref="AuditLog"/>. </summary>
        [ModelProperty("users")]
        public User[] Users { get; set; }
        /// <summary> List of <see cref="AuditLogEntry"/>. </summary>
        [ModelProperty("audit_log_entries")]
        public AuditLogEntry[] Entries { get; set; }
    }
}
