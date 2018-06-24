using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class RecipientEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("user")]
        public User User { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
    }
}
