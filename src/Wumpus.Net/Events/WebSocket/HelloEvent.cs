using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class HelloEvent
    {
        [ModelProperty("heartbeat_interval")]
        public int HeartbeatInterval { get; set; }
    }
}
