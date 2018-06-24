using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class SocketReaction
    {
        /// <summary> xxx </summary>
        [ModelProperty("user_id")]
        public ulong UserId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("message_id")]
        public ulong MessageId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public ulong ChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("emoji")]
        public Emoji Emoji { get; set; }
    }
}
