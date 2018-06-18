using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class ResumedEvent 
    { 
        [ModelProperty("heartbeat_interval")]
        public int HeartbeatInterval { get; set; }
    }
}
