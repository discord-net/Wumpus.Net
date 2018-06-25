using Voltaic.Serialization;

namespace Wumpus.Responses
{
    /// <summary> xxx </summary>
    public class GetBotGatewayResponse
    {
        /// <summary> xxx </summary>
        [ModelProperty("url")]
        public Utf8String Url { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("shards")]
        public int Shards { get; set; }
    }
}
