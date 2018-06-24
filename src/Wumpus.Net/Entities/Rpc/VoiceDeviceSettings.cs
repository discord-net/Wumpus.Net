using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class VoiceDeviceSettings
    {
        /// <summary> xxx </summary>
        [ModelProperty("device_id")]
        public Optional<string> DeviceId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("volume")]
        public Optional<float> Volume { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("available_devices")]
        public Optional<VoiceDevice[]> AvailableDevices { get; set; }
    }
}
