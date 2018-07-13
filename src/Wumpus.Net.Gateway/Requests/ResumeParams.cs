using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> 
    ///     Used to replay missed events when a disconnected client resumes.
    ///     https://discordapp.com/developers/docs/topics/gateway#resume 
    /// </summary>
    public class ResumeParams
    {
        /// <summary> Session token. </summary>
        [ModelProperty("token")]
        public Utf8String Token { get; set; }
        /// <summary> Session id. </summary>
        [ModelProperty("session_id")]
        public Utf8String SessionId { get; set; }
        /// <summary> Last sequence number received. </summary>
        [ModelProperty("seq")]
        public int Sequence { get; set; } 
    }
}
