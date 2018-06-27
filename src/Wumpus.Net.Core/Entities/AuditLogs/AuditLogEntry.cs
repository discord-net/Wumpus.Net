using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class AuditLogEntry
    {
        /// <summary> xxx </summary>
        [ModelProperty("target_id")]
        public Utf8String TargetId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("changes")]
        public AuditLogChange[] Changes { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("user_id")]
        public Snowflake UserId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("action_type")]
        public AuditLogEvent ActionType { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("options")]
        public Optional<OptionalAuditEntryInfo[]> Options { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("reason")]
        public Utf8String Reason { get; set; }
    }
}
