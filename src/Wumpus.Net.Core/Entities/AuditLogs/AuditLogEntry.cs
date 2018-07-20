using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/audit-log#audit-log-entry-object </summary>
    public class AuditLogEntry
    {
        /// <summary> Id of the affected entity. </summary>
        /// <remarks><see cref="Webhook"/>, <see cref="User"/>, <see cref="Role"/>, etc.</remarks>
        [ModelProperty("target_id")]
        public Utf8String TargetId { get; set; }
        /// <summary> Changes made to the <see cref="TargetId"/>. </summary>
        [ModelProperty("changes")]
        public Optional<AuditLogChange[]> Changes { get; set; }
        /// <summary> The <see cref="User"/> who made the changes. </summary>
        [ModelProperty("user_id")]
        public Snowflake UserId { get; set; }
        /// <summary> Id of the <see cref="AuditLogEntry"/>. </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> Type of action that occurred. </summary>
        [ModelProperty("action_type")]
        public AuditLogEvent ActionType { get; set; }
        /// <summary> Additional info for certain <see cref="ActionType"/>. </summary>
        [ModelProperty("options")]
        public Optional<OptionalAuditEntryInfo> Options { get; set; }
        /// <summary> The reason for the change. </summary>
        [ModelProperty("reason")]
        public Optional<Utf8String> Reason { get; set; }
    }
}
