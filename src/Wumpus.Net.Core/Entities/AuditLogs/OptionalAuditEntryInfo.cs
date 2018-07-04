using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/audit-log#audit-log-entry-object-optional-audit-entry-info </summary>
    public class OptionalAuditEntryInfo
    {
        /// <summary> Number of days after which inactive <see cref="GuildMember"/>s were kicked. Action Type: <see cref="AuditLogEvent.MemberPrune"/>. </summary>
        [ModelProperty("delete_member_days")]
        public Optional<Utf8String> DeleteMemberDays { get; set; }
        /// <summary> Number of <see cref="GuildMember"/>s removed by the prune. Action Type: <see cref="AuditLogEvent.MemberPrune"/>. </summary>
        [ModelProperty("members_removed")]
        public Optional<Utf8String> MembersRemoved { get; set; }
        /// <summary> <see cref="Channel"/> Id in which the <see cref="Message"/>s were deleted. Action Type: <see cref="AuditLogEvent.MessageDelete"/>. </summary>
        [ModelProperty("channel_id")]
        public Optional<Snowflake> ChannelId { get; set; }
        /// <summary> Number of deleted <see cref="Message"/>s. Action Type: <see cref="AuditLogEvent.MessageDelete"/></summary>
        [ModelProperty("count")]
        public Optional<Utf8String> Count { get; set; }
        /// <summary> Id of the overwritten entity. Action Types: <see cref="AuditLogEvent.ChannelOverwriteCreate"/> &amp; <see cref="AuditLogEvent.ChannelOverwriteUpdate"/> &amp; <see cref="AuditLogEvent.ChannelOverwriteDelete"/>. </summary>
        [ModelProperty("id")]
        public Optional<Snowflake> Id { get; set; }
        /// <summary> Type of overwritten entity. (<see cref="GuildMember"/> or <see cref="Role"/>) Action Types: <see cref="AuditLogEvent.ChannelOverwriteCreate"/> &amp; <see cref="AuditLogEvent.ChannelOverwriteUpdate"/> &amp; <see cref="AuditLogEvent.ChannelOverwriteDelete"/> </summary>
        [ModelProperty("type")]
        public Optional<Utf8String> Type { get; set; }
        /// <summary> Name of the role if type is <see cref="Role"/>. Action Types: <see cref="AuditLogEvent.ChannelOverwriteCreate"/> &amp; <see cref="AuditLogEvent.ChannelOverwriteUpdate"/> &amp; <see cref="AuditLogEvent.ChannelOverwriteDelete"/> </summary>
        [ModelProperty("role_name")]
        public Optional<Utf8String> RoleName { get; set; }
    }
}
