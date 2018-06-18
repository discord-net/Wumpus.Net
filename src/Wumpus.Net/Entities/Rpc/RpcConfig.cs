using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class RpcConfig
    {
        [ModelProperty("cdn_host")]
        public string CdnHost { get; set; }
        [ModelProperty("api_endpoint")]
        public string ApiEndpoint { get; set; }
        [ModelProperty("environment")]
        public string Environment { get; set; }
    }
}
