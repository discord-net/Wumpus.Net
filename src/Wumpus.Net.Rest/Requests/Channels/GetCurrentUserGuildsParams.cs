using System.Collections.Generic;
using Voltaic;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class GetCurrentUserGuildsParams : QueryMap
    {
        /// <summary> xxx </summary>
        public Optional<int> Limit { get; set; }
        /// <summary> xxx </summary>
        public Optional<Snowflake> Before { get; set; }
        /// <summary> xxx </summary>
        public Optional<Snowflake> After { get; set; }

        public override IDictionary<string, object> GetQueryMap()
        {
            var dict = new Dictionary<string, object>();
            if (Limit.IsSpecified)
                dict["limit"] = Limit.Value.ToString();
            if (Before.IsSpecified)
                dict["before"] = Before.Value.ToString();
            if (After.IsSpecified)
                dict["after"] = After.Value.ToString();
            return dict;
        }

        public void Validate()
        {
            Preconditions.NotNegative(Limit, nameof(Limit));
        }
    }
}
