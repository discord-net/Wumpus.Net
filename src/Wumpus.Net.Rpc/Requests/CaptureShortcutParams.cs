using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    [ModelStringEnum]
    public enum CaptureShortcutAction
    {
        [ModelEnumValue("START")] Start,
        [ModelEnumValue("STOP")] Stop
    }

    /// <summary> xxx </summary>
    public class CaptureShortcutParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("action")]
        public CaptureShortcutAction Action { get; set; }
    }
}
