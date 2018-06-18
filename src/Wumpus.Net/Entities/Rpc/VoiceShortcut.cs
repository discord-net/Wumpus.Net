using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class VoiceShortcut
    {
        [ModelProperty("type")]
        public Optional<VoiceShortcutType> Type { get; set; }
        [ModelProperty("code")]
        public Optional<int> Code { get; set; }
        [ModelProperty("name")]
        public Optional<string> Name { get; set; }
    }
}
