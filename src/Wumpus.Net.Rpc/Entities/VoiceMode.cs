using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class VoiceMode
    {
        /// <summary> xxx </summary>
        [ModelProperty("type")]
        public Optional<Utf8String> Type { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("auto_threshold")]
        public Optional<bool> AutoThreshold { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("threshold")]
        public Optional<float> Threshold { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("shortcut")]
        public Optional<VoiceShortcut[]> Shortcut { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("delay")]
        public Optional<float> Delay { get; set; }
    }
}
