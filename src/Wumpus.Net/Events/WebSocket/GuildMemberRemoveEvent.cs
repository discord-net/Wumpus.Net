using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class GuildMemberRemoveEvent
    {
        [ModelProperty("guild_id")]
        public ulong GuildId { get; set; }
        [ModelProperty("user")]
        public User User { get; set; }
    }
}
