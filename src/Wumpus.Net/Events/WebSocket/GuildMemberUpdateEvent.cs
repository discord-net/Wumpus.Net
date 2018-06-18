using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class GuildMemberUpdateEvent : GuildMember
    {
        [ModelProperty("guild_id")]
        public ulong GuildId { get; set; }
    }
}
