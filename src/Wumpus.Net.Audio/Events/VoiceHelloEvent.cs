using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class VoiceHelloEvent
    {
        // Given as float because discord returns json of the form
        // {"op":8,"d":{"v":4,"heartbeat_interval":13750.0}}
        [ModelProperty("heartbeat_interval")]
        public float HeartbeatInterval { get; set; }

        [ModelProperty("v")]
        public int GatewayVersion { get; set; }
    }
}