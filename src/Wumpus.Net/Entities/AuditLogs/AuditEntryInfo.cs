using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class AuditEntryInfo
    {
        [ModelProperty("delete_member_days")]
        public Optional<string> DeleteMemberDays { get; set; }
        [ModelProperty("members_removed")]
        public Optional<string> MembersRemoved { get; set; }
        [ModelProperty("channel_id")]
        public Optional<ulong> ChannelId { get; set; }
        [ModelProperty("count")]
        public Optional<string> Count { get; set; }
        [ModelProperty("id")]
        public Optional<ulong> Id { get; set; }
        [ModelProperty("type")]
        public Optional<string> Type { get; set; }
        [ModelProperty("role_name")]
        public Optional<string> RoleName { get; set; }
    }
}
