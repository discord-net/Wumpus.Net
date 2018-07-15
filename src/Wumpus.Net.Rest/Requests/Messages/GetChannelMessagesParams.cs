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

        public override IDictionary<string, object> GetQueryMap()
        {
            var dict = new Dictionary<string, object>();
            if (Limit.IsSpecified)
                dict["limit"] = Limit.Value.ToString();
            if (Around.IsSpecified)
                dict["around"] = Around.Value.ToString();
            if (Before.IsSpecified)
                dict["before"] = Before.Value.ToString();
            if (After.IsSpecified)
                dict["after"] = After.Value.ToString();
            return dict;
        }

        public void Validate()
        {
            Preconditions.NotNegative(Limit, nameof(Limit));
            Preconditions.Exclusive(new[] { Before, After, Around }, new[] { nameof(Before), nameof(After), nameof(Around) });
        }
    }
}
