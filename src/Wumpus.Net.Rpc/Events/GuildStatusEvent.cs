using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class GuildStatusEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("guild")]
        public Guild Guild { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("online")]
        public int Online { get; set; }
    }
}
