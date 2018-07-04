using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Responses
{
    /// <summary> https://discordapp.com/developers/docs/topics/gateway#get-gateway-example-response </summary>
    public class GetGatewayResponse
    {
        /// <summary> Gateway WSS url to use for connecting.  </summary>
        [ModelProperty("url")]
        public Utf8String Url { get; set; }
    }
}
