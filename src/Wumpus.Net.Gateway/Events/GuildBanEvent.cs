using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> 
    ///     Sent when a <see cref="Entities.User"/> is banned/unbanned from a <see cref="Guild"/>. The inner payload is a <see cref="Entities.User"/> object, with an extra guild_id key.
    ///     https://discordapp.com/developers/docs/topics/gateway#guild-ban-add, https://discordapp.com/developers/docs/topics/gateway#guild-ban-remove 
    /// </summary>
    public class GuildBanEvent : User
    {
        /// <summary> Id of the <see cref="Guild"/>. </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        /// <summary> The <see cref="Entities.User"/> for the <see cref="Ban"/>. </summary>
        [ModelProperty("user")]
        public User User { get; set; }
    }
}
