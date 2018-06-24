using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class HelloEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("heartbeat_interval")]
        public int HeartbeatInterval { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("_trace")]
        public string[] Trace { get; set; }
    }
}
