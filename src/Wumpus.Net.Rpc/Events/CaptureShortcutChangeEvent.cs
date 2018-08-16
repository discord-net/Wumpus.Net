using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class CaptureShortcutChangeEvent
    {
        [ModelProperty("shortcut")]
        public ShortcutKeyCombo[] Shortcut { get; set; }
    }
}