using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    [ModelStringEnum]
    public enum DeviceType
    {
        /// <summary> xxx </summary>
        [ModelEnumValue("AUDIO_INPUT")] AudioInput,
        /// <summary> xxx </summary>
        [ModelEnumValue("AUDIO_OUTPUT")] AudioOutput,
        /// <summary> xxx </summary>
        [ModelEnumValue("VIDEO_INPUT")] VideoInput
    }
}
