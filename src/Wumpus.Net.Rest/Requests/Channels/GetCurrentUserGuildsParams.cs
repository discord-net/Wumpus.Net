using System.Collections.Generic;
using Voltaic;
using Wumpus.Entities;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/user#get-current-user-guilds-query-string-params </summary>
    public class GetCurrentUserGuildsParams : QueryMap
    {
        /// <summary> Max number of <see cref="Guild"/>s to return. </summary>
        public Optional<int> Limit { get; set; }
        /// <summary> Get <see cref="Guild"/>s before this <see cref="Guild"/> id. </summary>
        public Optional<Snowflake> Before { get; set; }
        /// <summary> Get <see cref="Guild"/>s after this <see cref="Guild"/> id. </summary>
        public Optional<Snowflake> After { get; set; }

        public override IDictionary<string, string> CreateQueryMap()
        {
            var map = new Dictionary<string, string>();
            if (Limit.IsSpecified)
                map["limit"] = Limit.Value.ToString();
            if (Before.IsSpecified)
                map["before"] = Before.Value.ToString();
            if (After.IsSpecified)
                map["after"] = After.Value.ToString();
            return map;
        }
        public void LoadQueryMap(IReadOnlyDictionary<string, string> map)
        {
            if (map.TryGetValue("limit", out string str))
                Limit = int.Parse(str);
            if (map.TryGetValue("before", out str))
                Before = new Snowflake(ulong.Parse(str));
            if (map.TryGetValue("after", out str))
                After = new Snowflake(ulong.Parse(str));
        }

        public void Validate()
        {
            Preconditions.AtLeast(Limit, Guild.MinGetGuildsLimit, nameof(Limit));
            Preconditions.AtMost(Limit, Guild.MaxGetGuildsLimit, nameof(Limit));
            Preconditions.Exclusive(new[] { Before, After }, new[] { nameof(Before), nameof(After) });
        }
    }
}
