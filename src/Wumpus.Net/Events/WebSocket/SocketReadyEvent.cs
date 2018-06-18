using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class SocketReadyEvent
    {
        public class ReadState
        {
            [ModelProperty("id")]
            public string ChannelId { get; set; }
            [ModelProperty("mention_count")]
            public int MentionCount { get; set; }
            [ModelProperty("last_message_id")]
            public string LastMessageId { get; set; }
        }

        [ModelProperty("v")]
        public int Version { get; set; }
        [ModelProperty("user")]
        public User User { get; set; }
        [ModelProperty("session_id")]
        public string SessionId { get; set; }
        [ModelProperty("read_state")]
        public ReadState[] ReadStates { get; set; }
        [ModelProperty("guilds")]
        public SocketGuild[] Guilds { get; set; }
        [ModelProperty("private_channels")]
        public Channel[] PrivateChannels { get; set; }
    }
}
