using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class GuildRoleCreateEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("guild_id")]
        public ulong GuildId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("role")]
        public Role Role { get; set; }
    }
}
