using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class VoiceSpeakingParams
    {
        [ModelProperty("speaking")]
        public int Speaking { get; set; }

        [ModelProperty("delay")]
        public uint DelayMilliseconds { get; set; }

        [ModelProperty("ssrc")]
        public uint Ssrc { get; set; }
    }
}