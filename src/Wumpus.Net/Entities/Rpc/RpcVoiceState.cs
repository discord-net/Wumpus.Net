using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class RpcVoiceState
    {
        [ModelProperty("user")]
        public User User { get; set; }
        [ModelProperty("voice_state")]
        public Optional<VoiceState> VoiceState { get; set; }
        [ModelProperty("nick")]
        public Optional<string> Nickname { get; set; }
        [ModelProperty("volume")]
        public Optional<int> Volume { get; set; }
        [ModelProperty("mute")]
        public Optional<bool> Mute { get; set; }
        [ModelProperty("pan")]
        public Optional<Pan> Pan { get; set; }
    }
}
