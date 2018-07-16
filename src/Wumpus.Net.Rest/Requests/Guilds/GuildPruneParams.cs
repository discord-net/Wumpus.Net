using System.Collections.Generic;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#get-guild-prune-count-query-string-params </summary>
    public class GuildPruneParams : QueryMap
    {
        /// <summary> Number of days to count prune for. </summary>
        public int Days { get; set; }

        public GuildPruneParams(int days)
        {
            Days = days;
        }

        public override IDictionary<string, string> CreateQueryMap()
        {
            var map = new Dictionary<string, string>
            {
                ["days"] = Days.ToString()
            };
            return map;
        }
        public void LoadQueryMap(IReadOnlyDictionary<string, string> map)
        {
            if (map.TryGetValue("days", out string str))
                Days = int.Parse(str);
        }

        public void Validate()
        {
            Preconditions.NotNegative(Days, nameof(Days));
        }
    }
}
