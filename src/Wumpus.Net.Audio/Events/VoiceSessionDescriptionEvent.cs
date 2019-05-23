using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class VoiceSessionDescriptionEvent
    {
        [ModelProperty("video_codec")]
        public Utf8String VideoCodec { get; set; }

        [ModelProperty("secret_key")]
        public byte[] SecretKey { get; set; }

        [ModelProperty("mode")]
        public Utf8String EncryptionScheme { get; set; }

        [ModelProperty("media_session_id")]
        public Utf8String VideoSessionId { get; set; }

        [ModelProperty("audio_codec")]
        public Utf8String AudioCodec { get; set; }
    }
}