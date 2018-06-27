using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/topics/gateway#update-status </summary>
    public class UpdateStatusParams
    {
        /// <summary> unix time (in milliseconds) of when the client went idle, or null if the client is not idle </summary>
        [ModelProperty("since"), Int53]
        public long? Since { get; set; }
        /// <summary> null, or the user's new activity </summary>
        [ModelProperty("game")]
        public Activity Game { get; set; }
        /// <summary> the user's new status </summary>
        [ModelProperty("status")]
        public UserStatus Status { get; set; }
        /// <summary> whether or not the client is afk </summary>
        [ModelProperty("afk")]
        public bool AFK { get; set; }
    }
}
