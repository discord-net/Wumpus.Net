using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class TypingStartEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("user_id")]
        public Snowflake UserId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("timestamp")]
        public int Timestamp { get; set; }
    }
}
