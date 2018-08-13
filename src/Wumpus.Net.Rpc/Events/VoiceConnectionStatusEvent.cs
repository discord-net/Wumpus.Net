using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class VoiceConnectionStatusEvent
    {
        [ModelProperty("state")]
        public VoiceConnectionState State { get; set; }
        [ModelProperty("hostname")]
        public Utf8String Hostname { get; set; }
        [ModelProperty("pings")]
        public int[] Pings { get; set; }
        [ModelProperty("average_ping")]
        public int AveragePing { get; set; }
        [ModelProperty("last_ping")]
        public int LastPing { get; set; }
    }
}