using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#guild-embed-object </summary>
    public class GuildEmbed
    {
        /// <summary> If the <see cref="GuildEmbed"/> is enabled. </summary>
        [ModelProperty("enabled")]
        public bool Enabled { get; set; }
        /// <summary> The embed <see cref="Channel" /> Id. </summary>
        [ModelProperty("channel_id")]
        public Snowflake? ChannelId { get; set; }
    }
}
