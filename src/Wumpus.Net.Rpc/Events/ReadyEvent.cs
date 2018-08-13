using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class ReadyEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("v")]
        public int Version { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("config")]
        public RpcServerConfig Config { get; set; }
    }
}
