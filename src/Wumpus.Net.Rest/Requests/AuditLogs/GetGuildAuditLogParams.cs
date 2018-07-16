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

        public override IDictionary<string, object> GetQueryMap()
        {
            var dict = new Dictionary<string, object>();
            if (UserId.IsSpecified)
                dict["user_id"] = UserId;
            if (ActionType.IsSpecified)
                dict["action_type"] = (int)ActionType.Value;
            if (Before.IsSpecified)
                dict["before"] = Before.Value;
            if (Limit.IsSpecified)
                dict["limit"] = Limit.Value;
            return dict;
        }

        public void Validate()
        {
            Preconditions.NotZero(UserId, nameof(UserId));
            Preconditions.AtLeast(Limit, AuditLog.MinimumGetEntryAmount, nameof(Limit));
            Preconditions.AtMost(Limit, AuditLog.MaximumGetEntryAmount, nameof(Limit));
        }
    }
}
