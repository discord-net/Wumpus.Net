using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class GuildMemberAddEvent : GuildMember
    {
        /// <summary> xxx </summary>
        [ModelProperty("guild_id")]
        public ulong GuildId { get; set; }
    }
}
