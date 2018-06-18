using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class GuildStatusEvent
    {
        [ModelProperty("guild")]
        public Guild Guild { get; set; }
        [ModelProperty("online")]
        public int Online { get; set; }
    }
}
