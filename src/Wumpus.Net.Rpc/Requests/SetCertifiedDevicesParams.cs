using Voltaic;
using Voltaic.Serialization;
using Wumpus.Entities;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class SetCertifiedDevicesParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("type")]
        public DeviceType Type { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Utf8String Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("vendor")]
        public DeviceVendor Vendor { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("model")]
        public DeviceModel Model { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("related")]
        public Utf8String[] Related { get; set; }

        /// <summary> xxx </summary>
        [ModelProperty("echo_cancellation")]
        public Optional<bool> EchoCancellation { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("noise_suppression")]
        public Optional<bool> NoiseSuppression { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("automatic_gain_control")]
        public Optional<bool> AutomaticGainControl { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("hardware_mute")]
        public Optional<bool> HardwareMute { get; set; }
    }
}
