using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class SelectChannelParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public Snowflake? ChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("force")]
        public Optional<bool> Force { get; set; }
    }
}
