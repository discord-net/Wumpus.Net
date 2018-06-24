using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class VoiceState
    {
        /// <summary> xxx </summary>
        [ModelProperty("guild_id")]
        public Snowflake? GuildId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public Snowflake? ChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("user_id")]
        public Snowflake UserId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("session_id")]
        public Utf8String SessionId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("deaf")]
        public bool Deaf { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("mute")]
        public bool Mute { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("self_deaf")]
        public bool SelfDeaf { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("self_mute")]
        public bool SelfMute { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("suppress")]
        public bool Suppress { get; set; }
    }
}
