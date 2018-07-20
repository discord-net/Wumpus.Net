using System.Collections.Generic;
using Voltaic;
using Wumpus.Entities;

namespace Wumpus.Requests
{
    public class GetGuildAuditLogParams : QueryMap
    {
        /// <summary> Filter the log for a <see cref="User"/> id. </summary>
        public Optional<Snowflake> UserId { get; set; }
        /// <summary> The type of <see cref="AuditLogEvent"/>. </summary>
        public Optional<AuditLogEvent> ActionType { get; set; }
        /// <summary> Filter the log before a certain entry id.  </summary>
        public Optional<Snowflake> Before { get; set; }
        /// <summary> How many entries are returned. </summary>
        /// <remarks> Default: 50, Minimum: 1, Maximum: 100 </remarks>
        public Optional<int> Limit { get; set; }

        public override IDictionary<string, string> CreateQueryMap()
        {
            var map = new Dictionary<string, string>();
            if (UserId.IsSpecified)
                map["user_id"] = UserId.Value.ToString();
            if (ActionType.IsSpecified)
                map["action_type"] = ((int)ActionType.Value).ToString();
            if (Before.IsSpecified)
                map["before"] = Before.Value.ToString();
            if (Limit.IsSpecified)
                map["limit"] = Limit.Value.ToString();
            return map;
        }
        public void LoadQueryMap(IReadOnlyDictionary<string, string> map)
        {
            if (map.TryGetValue("user_id", out string str))
                UserId = new Snowflake(ulong.Parse(str));
            if (map.TryGetValue("action_type", out str))
                ActionType = (AuditLogEvent)int.Parse(str);
            if (map.TryGetValue("limit", out str))
                Limit = int.Parse(str);
            if (map.TryGetValue("before", out str))
                Before = new Snowflake(ulong.Parse(str));
        }

        public void Validate()
        {
            Preconditions.NotZero(UserId, nameof(UserId));
            Preconditions.AtLeast(Limit, AuditLog.MinimumGetEntryAmount, nameof(Limit));
            Preconditions.AtMost(Limit, AuditLog.MaximumGetEntryAmount, nameof(Limit));
        }
    }
}
