using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class RpcReadyEvent
    {
        [ModelProperty("v")]
        public int Version { get; set; }
        [ModelProperty("config")]
        public RpcConfig Config { get; set; }
    }
}
