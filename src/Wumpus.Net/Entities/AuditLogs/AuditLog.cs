using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class AuditLog
    {
        [ModelProperty("webhooks")]
        public Webhook[] Webhooks { get; set; }
        [ModelProperty("users")]
        public User[] Users { get; set; }
        [ModelProperty("audit_log_entries")]
        public AuditLogEntry[] Entries { get; set; }
    }
}
