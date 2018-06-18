using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class ResumeParams
    {
        [ModelProperty("token")]
        public string Token { get; set; }
        [ModelProperty("session_id")]
        public string SessionId { get; set; }
        [ModelProperty("seq")]
        public int Sequence { get; set; }
    }
}
