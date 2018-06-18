using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class TypingStartEvent
    {
        [ModelProperty("user_id")]
        public ulong UserId { get; set; }
        [ModelProperty("channel_id")]
        public ulong ChannelId { get; set; }
        [ModelProperty("timestamp")]
        public int Timestamp { get; set; }
    }
}
