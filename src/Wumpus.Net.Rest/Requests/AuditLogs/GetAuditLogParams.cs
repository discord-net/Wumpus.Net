﻿using System.Collections.Generic;
using Voltaic;
using Wumpus.Entities;

namespace Wumpus.Requests
{
    /// <summary> 
    ///    Returns an <see cref="AuditLog"/> object for the <see cref="Guild"/>. Requires <see cref="GuildPermissions.ViewAuditLog"/>.
    ///    https://discordapp.com/developers/docs/resources/audit-log#get-guild-audit-log 
    /// </summary>
    public class GetAuditLogParams : QueryMap
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
                dict["userId"] = UserId.ToString();
            if (ActionType.IsSpecified)
                dict["actionType"] = ((int)ActionType.Value).ToString();
            if (Before.IsSpecified)
                dict["before"] = Before.Value.ToString();
            if (Limit.IsSpecified)
                dict["limit"] = Limit.Value.ToString();
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
