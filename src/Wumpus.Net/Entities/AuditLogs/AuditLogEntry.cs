using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class AuditLogEntry
    {
        [ModelProperty("target_id")]
        public string TargetId { get; set; }
        [ModelProperty("changes")]
        public AuditLogChange[] Changes { get; set; }
        [ModelProperty("user_id")]
        public ulong UserId { get; set; }
        [ModelProperty("id")]
        public ulong Id { get; set; }
        [ModelProperty("action_type")]
        public AuditLogEvent ActionType { get; set; }
        [ModelProperty("options")]
        public Optional<AuditEntryInfo[]> Options { get; set; }
        [ModelProperty("reason")]
        public string Reason { get; set; }
    }
}
