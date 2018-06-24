using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/topics/gateway#resume </summary>
    public class ResumeParams
    {
        /// <summary> session token </summary>
        [ModelProperty("token")]
        public string Token { get; set; }
        /// <summary> session id </summary>
        [ModelProperty("session_id")]
        public string SessionId { get; set; }
        /// <summary> last sequence number received </summary>
        [ModelProperty("seq")]
        public int Sequence { get; set; } 
    }
}
