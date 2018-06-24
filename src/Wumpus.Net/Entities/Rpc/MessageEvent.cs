using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class MessageEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public ulong ChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("message")]
        public Message Message { get; set; }
    }
}
