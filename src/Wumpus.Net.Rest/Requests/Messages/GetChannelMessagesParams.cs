using System.Collections.Generic;
using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class GetChannelMessagesParams : QueryMap
    {
        /// <summary> Get <see cref="Entities.Message"/>s before this id. </summary>
        [ModelProperty("before")]
        public Optional<Snowflake> Before { get; set; }
        /// <summary> Get <see cref="Entities.Message"/>s around this id. </summary>
        [ModelProperty("around")]
        public Optional<Snowflake> Around { get; set; }
        /// <summary> Get <see cref="Entities.Message"/>s after this id. </summary>
        [ModelProperty("after")]
        public Optional<Snowflake> After { get; set; }
        /// <summary> Max number of <see cref="Entities.Message"/>s to return. </summary>
        /// <remarks> Minimum: 1, Default: 50, Maximum: 100 </remarks>
        [ModelProperty("limit")]
        public Optional<int> Limit { get; set; }

        public override IDictionary<string, string> CreateQueryMap()
        {
            var map = new Dictionary<string, string>();
            if (Limit.IsSpecified)
                map["limit"] = Limit.Value.ToString();
            if (Before.IsSpecified)
                map["before"] = Before.Value.ToString();
            if (Around.IsSpecified)
                map["around"] = Around.Value.ToString();
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
            if (map.TryGetValue("around", out str))
                Around = new Snowflake(ulong.Parse(str));
            if (map.TryGetValue("after", out str))
                After = new Snowflake(ulong.Parse(str));
        }

        public void Validate()
        {
            Preconditions.NotNegative(Limit, nameof(Limit));
            Preconditions.Exclusive(new[] { Before, After, Around }, new[] { nameof(Before), nameof(After), nameof(Around) });
        }
    }
}
