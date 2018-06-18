using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class RemoveAllReactionsEvent
    {
        [ModelProperty("channel_id")]
        public ulong ChannelId { get; set; }
        [ModelProperty("message_id")]
        public ulong MessageId { get; set; }
    }
}
