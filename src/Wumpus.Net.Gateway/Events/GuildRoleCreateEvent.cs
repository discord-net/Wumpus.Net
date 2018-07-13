using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> 
    ///     Sent when a <see cref="Guild"/> <see cref="Entities.Role"/> is created.
    ///     https://discordapp.com/developers/docs/topics/gateway#guild-role-create
    /// </summary>
    public class GuildRoleCreateEvent
    {
        /// <summary> The id of the <see cref="Guild"/>. </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        /// <summary> The <see cref="Entities.Role"/> created. </summary>
        [ModelProperty("role")]
        public Role Role { get; set; }
    }
}
