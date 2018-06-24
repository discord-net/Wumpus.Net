using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class GuildRoleDeleteEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("guild_id")]
        public ulong GuildId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("role_id")]
        public ulong RoleId { get; set; }
    }
}
