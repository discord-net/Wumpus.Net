using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> 
    ///     Sent when a <see cref="Entities.User"/> is banned from a <see cref="Guild"/>. The inner payload is a <see cref="Entities.User"/> object, with an extra guild_id key.
    ///     https://discordapp.com/developers/docs/topics/gateway#guild-ban-add, https://discordapp.com/developers/docs/topics/gateway#guild-ban-remove 
    /// </summary>
    public class GuildBanAddEvent : User
    {
        /// <summary> Id of the <see cref="Guild"/>. </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
    }
}
