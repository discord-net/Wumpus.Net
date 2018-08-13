using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class VoiceChannelSelectEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
    }
}
