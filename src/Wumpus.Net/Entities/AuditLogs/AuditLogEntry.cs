using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class AuditLogEntry
    {
        /// <summary> xxx </summary>
        [ModelProperty("target_id")]
        public string TargetId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("changes")]
        public AuditLogChange[] Changes { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("user_id")]
        public ulong UserId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public ulong Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("action_type")]
        public AuditLogEvent ActionType { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("options")]
        public Optional<AuditEntryInfo[]> Options { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("reason")]
        public string Reason { get; set; }
    }
}
