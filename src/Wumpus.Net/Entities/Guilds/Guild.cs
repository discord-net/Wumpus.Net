using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class Guild
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public ulong Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public string Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("icon")]
        public string Icon { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("splash")]
        public string Splash { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("owner_id")]
        public ulong OwnerId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("region")]
        public string Region { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("afk_channel_id")]
        public ulong? AFKChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("afk_timeout")]
        public int AFKTimeout { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("embed_enabled")]
        public bool EmbedEnabled { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("embed_channel_id")]
        public ulong? EmbedChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("verification_level")]
        public VerificationLevel VerificationLevel { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("default_message_notifications")]
        public DefaultMessageNotifications DefaultMessageNotifications { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("explicit_content_filter")]
        public ExplicitContentFilter ExplicitContentFilter { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("roles")]
        public Role[] Roles { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("emojis")]
        public Emoji[] Emojis { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("features")]
        public string[] Features { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("mfa_level")]
        public MfaLevel MfaLevel { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("application_id")]
        public ulong? ApplicationId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("widget_enabled")]
        public bool IsWidgetEnabled { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("widget_channel_id")]
        public ulong WidgetChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("voice_states")]
        public VoiceState[] VoiceStates { get; set; }
    }
}
