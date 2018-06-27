using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class RpcVoiceState
    {
        /// <summary> xxx </summary>
        [ModelProperty("user")]
        public User User { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("voice_state")]
        public Optional<VoiceState> VoiceState { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("nick")]
        public Optional<Utf8String> Nickname { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("volume")]
        public Optional<int> Volume { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("mute")]
        public Optional<bool> Mute { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("pan")]
        public Optional<Pan> Pan { get; set; }
    }
}
