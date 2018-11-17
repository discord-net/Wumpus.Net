using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class VoiceReadyEvent
    {
        [ModelProperty("ssrc")]
        public uint Ssrc { get; set; }

        [ModelProperty("port")]
        public int Port { get; set; }

        [ModelProperty("modes")]
        public Utf8String[] EncryptionSchemes { get; set; }

        [ModelProperty("ip")]
        public Utf8String IpAddress { get; set; }
    }
}