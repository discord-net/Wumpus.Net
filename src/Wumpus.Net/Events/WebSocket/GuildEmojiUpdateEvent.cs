using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class GuildEmojiUpdateEvent
    {
        [ModelProperty("guild_id")]
        public ulong GuildId { get; set; }
        [ModelProperty("emojis")]
        public Emoji[] Emojis { get; set; }
    }
}
