using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class MessageEvent
    {
        [ModelProperty("channel_id")]
        public ulong ChannelId { get; set; }
        [ModelProperty("message")]
        public Message Message { get; set; }
    }
}
