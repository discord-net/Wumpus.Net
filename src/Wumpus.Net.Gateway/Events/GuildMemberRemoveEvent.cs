using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> 
    ///     Sent when a <see cref="Entities.User"/> is removed from a <see cref="Guild"/>. (Leave/Kick/Ban)
    ///     https://discordapp.com/developers/docs/topics/gateway#guild-member-remove 
    /// </summary>
    public class GuildMemberRemoveEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("user")]
        public User User { get; set; }
    }
}
