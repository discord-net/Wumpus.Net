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

        public override IDictionary<string, object> GetQueryMap()
        {
            var dict = new Dictionary<string, object>
            {
                ["days"] = Days
            };
            return dict;
        }

        public void Validate()
        {
            Preconditions.NotNegative(Days, nameof(Days));
        }
    }
}
