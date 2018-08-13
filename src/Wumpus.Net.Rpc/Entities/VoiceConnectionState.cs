using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    [ModelStringEnum]
    public enum VoiceConnectionState
    {
        /// <summary> xxx </summary>
        [ModelEnumValue("DISCONNECTED")] Disconnected,
        /// <summary> xxx </summary>
        [ModelEnumValue("AWAITING_ENDPOINT")] AwaitingEndpoint,
        /// <summary> xxx </summary>
        [ModelEnumValue("AUTHENTICATING")] Authenticating,
        /// <summary> xxx </summary>
        [ModelEnumValue("CONNECTING")] Connecting,
        /// <summary> xxx </summary>
        [ModelEnumValue("CONNECTED")] Connected,
        /// <summary> xxx </summary>
        [ModelEnumValue("VOICE_DISCONNECTED")] VoiceDisconnected,
        /// <summary> xxx </summary>
        [ModelEnumValue("VOICE_CONNECTING")] VoiceConnecting,
        /// <summary> xxx </summary>
        [ModelEnumValue("VOICE_CONNECTED")] VoiceConnected,
        /// <summary> xxx </summary>
        [ModelEnumValue("NO_ROUTE")] NoRoute,
        /// <summary> xxx </summary>
        [ModelEnumValue("ICE_CHECKING")] IceChecking,
    }
}
