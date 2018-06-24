using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class GuildEmojiUpdateEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("emojis")]
        public Emoji[] Emojis { get; set; }
    }
}
