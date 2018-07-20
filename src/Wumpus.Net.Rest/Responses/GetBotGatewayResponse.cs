using Voltaic.Serialization;

namespace Wumpus.Responses
{
    /// <summary> https://discordapp.com/developers/docs/topics/gateway#get-gateway-bot-example-response </summary>
    public class GetBotGatewayResponse : GetGatewayResponse
    {
        /// <summary> Recommended number of shards to connect with. </summary>
        [ModelProperty("shards")]
        public int Shards { get; set; }
    }
}
