using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class AuditEntryInfo
    {
        /// <summary> xxx </summary>
        [ModelProperty("delete_member_days")]
        public Optional<Utf8String> DeleteMemberDays { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("members_removed")]
        public Optional<Utf8String> MembersRemoved { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public Optional<ulong> ChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("count")]
        public Optional<string> Count { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Optional<ulong> Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("type")]
        public Optional<Utf8String> Type { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("role_name")]
        public Optional<Utf8String> RoleName { get; set; }
    }
}
