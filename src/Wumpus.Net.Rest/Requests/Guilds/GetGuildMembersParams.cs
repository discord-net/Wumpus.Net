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

        public override IDictionary<string, string> CreateQueryMap()
        {
            var map = new Dictionary<string, string>();
            if (Limit.IsSpecified)
                map["limit"] = Limit.Value.ToString();
            if (After.IsSpecified)
                map["after"] = After.Value.ToString();
            return map;
        }
        public void LoadQueryMap(IReadOnlyDictionary<string, string> map)
        {
            if (map.TryGetValue("limit", out string str))
                Limit = int.Parse(str);
            if (map.TryGetValue("after", out str))
                After = new Snowflake(ulong.Parse(str));
        }

        public void Validate()
        {
            Preconditions.NotNegative(Limit, nameof(Limit));

            Preconditions.AtLeast(Limit, Guild.MinGetGuildsLimit, nameof(Limit));
            Preconditions.AtMost(Limit, Guild.MaxGetMembersLimit, nameof(Limit));
        }
    }
}
