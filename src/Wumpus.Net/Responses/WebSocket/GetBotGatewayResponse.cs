using Voltaic.Serialization;

namespace Wumpus.Responses
{
    public class GetBotGatewayResponse
    {
        [ModelProperty("url")]
        public string Url { get; set; }
        [ModelProperty("shards")]
        public int Shards { get; set; }
    }
}
