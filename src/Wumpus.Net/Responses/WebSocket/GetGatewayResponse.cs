using Voltaic.Serialization;

namespace Wumpus.Responses
{
    public class GetGatewayResponse
    {
        [ModelProperty("url")]
        public string Url { get; set; }
    }
}
