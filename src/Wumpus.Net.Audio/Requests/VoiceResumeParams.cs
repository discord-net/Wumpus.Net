using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class VoiceResumeParams
    {
        [ModelProperty("server_id")]
        public Snowflake GuildId { get; set; }

        [ModelProperty("session_id")]
        public Utf8String SessionId { get; set; }

        [ModelProperty("token")]
        public Utf8String Token { get; set; }
    }
}