using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class GuildRoleCreateEvent
    {
        [ModelProperty("guild_id")]
        public ulong GuildId { get; set; }
        [ModelProperty("role")]
        public Role Role { get; set; }
    }
}
