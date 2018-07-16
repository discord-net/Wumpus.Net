using System.Collections.Generic;
using Voltaic;
using Wumpus.Entities;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#list-guild-members-query-string-params </summary>
    public class GetGuildMembersParams : QueryMap
    {
        /// <summary> Max number of <see cref="Entities.GuildMember"/>s to return. </summary>
        public Optional<int> Limit { get; set; }
        /// <summary> The highest <see cref="Entities.User"/> id in the previous page. </summary>
        public Optional<Snowflake> After { get; set; }

        public override IDictionary<string, object> GetQueryMap()
        {
            var dict = new Dictionary<string, object>();
            if (Limit.IsSpecified)
                dict["limit"] = Limit.Value;
            if (After.IsSpecified)
                dict["after"] = After.Value;
            return dict;
        }

        public void Validate()
        {
            Preconditions.NotNegative(Limit, nameof(Limit));

            Preconditions.AtLeast(Limit, Guild.MinGetGuildsLimit, nameof(Limit));
            Preconditions.AtMost(Limit, Guild.MaxGetMembersLimit, nameof(Limit));
        }
    }
}
