using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class ResumedEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("heartbeat_interval")]
        public int HeartbeatInterval { get; set; }
    }
}
