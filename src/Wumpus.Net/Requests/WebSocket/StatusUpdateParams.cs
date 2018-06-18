using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Requests
{
    public class StatusUpdateParams
    {
        [ModelProperty("status")]
        public UserStatus Status { get; set; }
        [ModelProperty("since"), Int53]
        public long? IdleSince { get; set; }
        [ModelProperty("afk")]
        public bool IsAFK { get; set; }
        [ModelProperty("game")]
        public Game Game { get; set; }
    }
}
