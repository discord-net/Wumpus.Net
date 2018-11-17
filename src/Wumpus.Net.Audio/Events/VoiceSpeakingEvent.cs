using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class VoiceSpeakingEvent
    {
        [ModelProperty("delay")]
        public int DelayMilliseconds { get; set; }

        [ModelProperty("speaking")]
        public SpeakingState Speaking { get; set; }

        [ModelProperty("ssrc")]
        public uint Ssrc { get; set; }
    }
}