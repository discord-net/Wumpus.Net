using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> 
    ///     Sent when a guild's emojis have been updated. 
    ///     https://discordapp.com/developers/docs/topics/gateway#guild-emojis-update 
    /// </summary>
    public class GuildEmojiUpdateEvent
    {
        /// <summary> Id of the <see cref="Guild"/>. </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        /// <summary> Array of <see cref="Emoji"/>s. </summary>
        [ModelProperty("emojis")]
        public Emoji[] Emojis { get; set; }
    }
}
