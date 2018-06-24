using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class ChannelSubscriptionParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
    }
}
