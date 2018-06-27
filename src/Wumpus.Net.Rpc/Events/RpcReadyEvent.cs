using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class RpcReadyEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("v")]
        public int Version { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("config")]
        public RpcConfig Config { get; set; }
    }
}
