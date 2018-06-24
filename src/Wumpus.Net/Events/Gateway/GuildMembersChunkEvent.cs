using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class GuildMembersChunkEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("members")]
        public GuildMember[] Members { get; set; }
    }
}
