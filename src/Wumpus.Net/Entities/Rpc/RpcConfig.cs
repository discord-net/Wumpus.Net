using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class RpcConfig
    {
        /// <summary> xxx </summary>
        [ModelProperty("cdn_host")]
        public string CdnHost { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("api_endpoint")]
        public string ApiEndpoint { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("environment")]
        public string Environment { get; set; }
    }
}
