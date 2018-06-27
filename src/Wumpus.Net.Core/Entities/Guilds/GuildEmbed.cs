using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class GuildEmbed
    {
        /// <summary> xxx </summary>
        [ModelProperty("enabled")]
        public bool Enabled { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
    }
}
