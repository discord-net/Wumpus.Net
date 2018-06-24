using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class RemoveAllReactionsEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public ulong ChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("message_id")]
        public ulong MessageId { get; set; }
    }
}
