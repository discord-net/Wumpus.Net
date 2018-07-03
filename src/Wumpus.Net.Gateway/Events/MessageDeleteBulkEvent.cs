using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class MessageDeleteBulkEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("ids")]
        public Snowflake[] Ids { get; set; }
    }
}
