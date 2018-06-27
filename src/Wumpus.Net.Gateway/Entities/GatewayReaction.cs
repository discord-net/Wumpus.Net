using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class GatewayReaction
    {
        /// <summary> xxx </summary>
        [ModelProperty("user_id")]
        public Snowflake UserId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("message_id")]
        public Snowflake MessageId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("emoji")]
        public Emoji Emoji { get; set; }
    }
}
