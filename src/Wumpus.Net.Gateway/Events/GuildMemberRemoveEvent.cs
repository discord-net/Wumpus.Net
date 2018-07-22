using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> 
    ///     Sent when a <see cref="Entities.User"/> is removed from a <see cref="Guild"/> (leave/kick/ban).
    ///     https://discordapp.com/developers/docs/topics/gateway#guild-member-remove 
    /// </summary>
    public class GuildMemberRemoveEvent
    {
        /// <summary> The id of the <see cref="Entities.Guild"/>. </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        /// <summary> The <see cref="Entities.User"/> who was removed. </summary>
        [ModelProperty("user")]
        public User User { get; set; }
    }
}
