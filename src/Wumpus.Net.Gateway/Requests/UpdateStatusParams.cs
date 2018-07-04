using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Requests
{
    /// <summary>
    ///     Sent by the client to indicate a <see cref="Presence"/> or <see cref="UserStatus"/> update.
    ///     https://discordapp.com/developers/docs/topics/gateway#update-status 
    /// </summary>
    public class UpdateStatusParams
    {
        /// <summary> Unix time (in milliseconds) of when the client went idle, or null if the client is not idle. </summary>
        [ModelProperty("since"), Int53]
        public long? Since { get; set; }
        /// <summary> Null, or the <see cref="User"/>'s new <see cref="Activity"/>. </summary>
        [ModelProperty("game")]
        public Activity Game { get; set; }
        /// <summary> The <see cref="User"/>'s new <see cref="UserStatus"/>. </summary>
        [ModelProperty("status")]
        public UserStatus Status { get; set; }
        /// <summary> Whether or not the client is afk. </summary>
        [ModelProperty("afk")]
        public bool AFK { get; set; }
    }
}
