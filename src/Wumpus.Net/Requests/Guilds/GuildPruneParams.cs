using System.Collections.Generic;

namespace Wumpus.Requests
{
    public class GuildPruneParams : QueryMap
    {
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
