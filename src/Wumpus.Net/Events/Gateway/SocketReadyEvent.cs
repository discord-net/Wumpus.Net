using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class SocketReadyEvent
    {
        /// <summary> xxx </summary>
        public class ReadState
        {
            /// <summary> xxx </summary>
            [ModelProperty("id")]
            public string ChannelId { get; set; }
            /// <summary> xxx </summary>
            [ModelProperty("mention_count")]
            public int MentionCount { get; set; }
            /// <summary> xxx </summary>
            [ModelProperty("last_message_id")]
            public string LastMessageId { get; set; }
        }

        /// <summary> xxx </summary>
        [ModelProperty("v")]
        public int Version { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("user")]
        public User User { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("session_id")]
        public string SessionId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("read_state")]
        public ReadState[] ReadStates { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("guilds")]
        public SocketGuild[] Guilds { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("private_channels")]
        public Channel[] PrivateChannels { get; set; }
    }
}
