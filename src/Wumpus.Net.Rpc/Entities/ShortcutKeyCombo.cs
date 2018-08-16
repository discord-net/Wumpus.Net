using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class ShortcutKeyCombo
    {
        /// <summary> xxx </summary>
        [ModelProperty("type")]
        public Optional<VoiceShortcutType> Type { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("code")]
        public Optional<int> Code { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Optional<Utf8String> Name { get; set; }
    }
}
