using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class SocketReaction
    {
        [ModelProperty("user_id")]
        public ulong UserId { get; set; }
        [ModelProperty("message_id")]
        public ulong MessageId { get; set; }
        [ModelProperty("channel_id")]
        public ulong ChannelId { get; set; }
        [ModelProperty("emoji")]
        public Emoji Emoji { get; set; }
    }
}
