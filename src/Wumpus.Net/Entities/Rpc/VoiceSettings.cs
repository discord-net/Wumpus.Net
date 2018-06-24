
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class VoiceSettings
    {
        /// <summary> xxx </summary>
        [ModelProperty("input")]
        public VoiceDeviceSettings Input { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("output")]
        public VoiceDeviceSettings Output { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("mode")]
        public VoiceMode Mode { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("automatic_gain_control")]
        public Optional<bool> AutomaticGainControl { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("echo_cancellation")]
        public Optional<bool> EchoCancellation { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("noise_suppression")]
        public Optional<bool> NoiseSuppression { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("qos")]
        public Optional<bool> QualityOfService { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("silence_warning")]
        public Optional<bool> SilenceWarning { get; set; }
    }
}
