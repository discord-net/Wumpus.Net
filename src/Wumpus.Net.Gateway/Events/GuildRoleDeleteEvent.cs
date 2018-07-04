using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> 
    ///     Sent when a <see cref="Entities.Guild"/> <see cref="Entities.Role"/> is deleted.
    /// </summary>
    public class GuildRoleDeleteEvent
    {
        /// <summary> Id of the <see cref="Entities.Guild"/>. </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        /// <summary> Id of the <see cref="Entities.Role"/>. </summary>
        [ModelProperty("role_id")]
        public Snowflake RoleId { get; set; }
    }
}
