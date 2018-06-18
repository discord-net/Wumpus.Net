using Voltaic.Serialization;
using System.Collections.Generic;

namespace Wumpus.Requests
{
    public class IdentifyParams
    {
        [ModelProperty("token")]
        public string Token { get; set; }
        [ModelProperty("properties")]
        public Dictionary<string, string> Properties { get; set; }
        [ModelProperty("large_threshold")]
        public int LargeThreshold { get; set; }
        [ModelProperty("compress")]
        public bool UseCompression { get; set; }
        [ModelProperty("shard")]
        public Optional<int[]> ShardingParams { get; set; }
    }
}
