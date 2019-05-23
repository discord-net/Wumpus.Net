using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class VoiceSelectProtocolParams
    {
        [ModelProperty("protocol")]
        public Utf8String TransportProtocol { get; set; }

        [ModelProperty("data")]
        public VoiceSelectProtocolConnectionProperties Properties { get; set; }
    }

    public class VoiceSelectProtocolConnectionProperties
    {
        [ModelProperty("ip")]
        public Utf8String Ip { get; set; }

        [ModelProperty("port")]
        public int Port { get; set; }

        [ModelProperty("mode")]
        public Utf8String EncryptionScheme { get; set; }
    }
}