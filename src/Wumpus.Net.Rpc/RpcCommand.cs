using Voltaic.Serialization;

namespace Wumpus
{
    /// <summary> xxx </summary>
    [ModelStringEnum]
    public enum RpcCommand : byte
    {
        /// <summary> Event dispatch </summary>
        [ModelEnumValue("DISPATCH")] Dispatch,
        /// <summary> xxx </summary>
        [ModelEnumValue("AUTHORIZE")] Authorize,
        /// <summary> xxx </summary>
        [ModelEnumValue("AUTHENTICATE")] Authenticate,
        /// <summary> xxx </summary>
        [ModelEnumValue("GET_GUILD")] GetGuild,
        /// <summary> xxx </summary>
        [ModelEnumValue("GET_GUILDS")] GetGuilds,
        /// <summary> xxx </summary>
        [ModelEnumValue("GET_CHANNEL")] GetChannel,
        /// <summary> xxx </summary>
        [ModelEnumValue("GET_CHANNELS")] GetChannels,
        /// <summary> xxx </summary>
        [ModelEnumValue("SUBSCRIBE")] Subscribe,
        /// <summary> xxx </summary>
        [ModelEnumValue("UNSUBSCRIBE")] Unsubscribe,
        /// <summary> xxx </summary>
        [ModelEnumValue("SET_USER_VOICE_SETTINGS")] SetUserVoiceSettings,
        /// <summary> xxx </summary>
        [ModelEnumValue("SELECT_VOICE_CHANNEL")] SelectVoiceChannel,
        /// <summary> xxx </summary>
        [ModelEnumValue("GET_SELECTED_VOICE_CHANNEL")] GetSelectedVoiceChannel,
        /// <summary> xxx </summary>
        [ModelEnumValue("SELECT_TEXT_CHANNEL")] SelectTextChannel,
        /// <summary> xxx </summary>
        [ModelEnumValue("GET_VOICE_SETTINGS")] GetVoiceSettings,
        /// <summary> xxx </summary>
        [ModelEnumValue("SET_VOICE_SETTINGS")] SetVoiceSettings,
        /// <summary> xxx </summary>
        [ModelEnumValue("CAPTURE_SHORTCUT")] CaptureShortcut,
        /// <summary> xxx </summary>
        [ModelEnumValue("SET_CERTIFIED_DEVICES")] SetCertifiedDevices
    }
}