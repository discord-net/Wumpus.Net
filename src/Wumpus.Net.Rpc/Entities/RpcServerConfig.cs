using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class RpcServerConfig
    {
        /// <summary> xxx </summary>
        [ModelProperty("cdn_host")]
        public Utf8String CdnHost { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("api_endpoint")]
        public Utf8String ApiEndpoint { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("environment")]
        public Utf8String Environment { get; set; }
    }
}
