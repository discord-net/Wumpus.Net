using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/audit-log#audit-log-object </summary>
    public class AuditLog
    {
        /// <summary> xxx </summary>
        [ModelProperty("webhooks")]
        public Webhook[] Webhooks { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("users")]
        public User[] Users { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("audit_log_entries")]
        public AuditLogEntry[] Entries { get; set; }
    }
}
