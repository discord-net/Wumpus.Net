using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class Guild
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("icon")]
        public Utf8String Icon { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("splash")]
        public Utf8String Splash { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("owner_id")]
        public Snowflake OwnerId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("region")]
        public Utf8String Region { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("afk_channel_id")]
        public Snowflake? AFKChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("afk_timeout")]
        public int AFKTimeout { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("embed_enabled")]
        public bool EmbedEnabled { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("embed_channel_id")]
        public Snowflake? EmbedChannelId { get; set; }
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
        public Utf8String[] Features { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("mfa_level")]
        public MfaLevel MfaLevel { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("application_id")]
        public Snowflake? ApplicationId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("widget_enabled")]
        public bool IsWidgetEnabled { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("widget_channel_id")]
        public Snowflake WidgetChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("voice_states")]
        public VoiceState[] VoiceStates { get; set; }
    }
}
