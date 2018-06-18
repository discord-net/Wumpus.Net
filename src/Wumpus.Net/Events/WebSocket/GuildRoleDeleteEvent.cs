using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class GuildRoleDeleteEvent
    {
        [ModelProperty("guild_id")]
        public ulong GuildId { get; set; }
        [ModelProperty("role_id")]
        public ulong RoleId { get; set; }
    }
}
