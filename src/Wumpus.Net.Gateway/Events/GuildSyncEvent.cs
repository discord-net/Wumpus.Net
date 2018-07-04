using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    // TODO: Remove, undocumented?
    /// <summary> xxx </summary>
    public class GuildSyncEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("large")]
        public bool Large { get; set; }

        /// <summary> xxx </summary>
        [ModelProperty("presences")]
        public Presence[] Presences { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("members")]
        public GuildMember[] Members { get; set; }
    }
}
