using Voltaic.Serialization;

namespace Wumpus
{
    /// <summary> xxx </summary>
    [ModelStringEnum]
    public enum RpcEvent : byte
    {
        /// <summary> xxx </summary>
        [ModelEnumValue("READY")] Ready,
        /// <summary> xxx </summary>
        [ModelEnumValue("ERROR")] Error,
        /// <summary> xxx </summary>
        [ModelEnumValue("GUILD_STATUS")] GuildStatus,
        /// <summary> xxx </summary>
        [ModelEnumValue("GUILD_CREATE")] GuildCreate,
        /// <summary> xxx </summary>
        [ModelEnumValue("CHANNEL_CREATE")] ChannelCreate,
        /// <summary> xxx </summary>
        [ModelEnumValue("VOICE_CHANNEL_SELECT")] VoiceChannelSelect,
        /// <summary> xxx </summary>
        [ModelEnumValue("VOICE_STATE_CREATE")] VoiceStateCreate,
        /// <summary> xxx </summary>
        [ModelEnumValue("VOICE_STATE_UPDATE")] VoiceStateUpdate,
        /// <summary> xxx </summary>
        [ModelEnumValue("VOICE_STATE_DELETE")] VoiceStateDelete,
        /// <summary> xxx </summary>
        [ModelEnumValue("VOICE_SETTINGS_UPDATE")] VoiceSettingsUpdate,
        /// <summary> xxx </summary>
        [ModelEnumValue("VOICE_CONNECTION_STATUS")] VoiceConnectionStatus,
        /// <summary> xxx </summary>
        [ModelEnumValue("SPEAKING_START")] SpeakingStart,
        /// <summary> xxx </summary>
        [ModelEnumValue("SPEAKING_STOP")] SpeakingStop,
        /// <summary> xxx </summary>
        [ModelEnumValue("MESSAGE_CREATE")] MessageCreate,
        /// <summary> xxx </summary>
        [ModelEnumValue("MESSAGE_UPDATE")] MessageUpdate,
        /// <summary> xxx </summary>
        [ModelEnumValue("MESSAGE_DELETE")] MessageDelete,
        /// <summary> xxx </summary>
        [ModelEnumValue("NOTIFICATION_CREATE")] NotificationCreate,
        /// <summary> xxx </summary>
        [ModelEnumValue("CAPTURE_SHORTCUT_CHANGE")] CaptureShortcutChange
    }
}
