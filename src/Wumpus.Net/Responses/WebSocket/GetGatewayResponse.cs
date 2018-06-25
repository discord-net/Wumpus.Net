using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Responses
{
    /// <summary> xxx </summary>
    public class GetGatewayResponse
    {
        /// <summary> xxx </summary>
        [ModelProperty("url")]
        public Utf8String Url { get; set; }
    }
}
