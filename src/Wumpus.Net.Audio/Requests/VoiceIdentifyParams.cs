using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class VoiceIdentifyParams
    {
        [ModelProperty("user_id")]
        public Snowflake UserId { get; set; }
        [ModelProperty("server_id")]
        public Snowflake GuildId { get; set; }
        [ModelProperty("session_id")]
        public Utf8String SessionId { get; set; }
        [ModelProperty("token")]
        public Utf8String Token { get; set; }
    }
}