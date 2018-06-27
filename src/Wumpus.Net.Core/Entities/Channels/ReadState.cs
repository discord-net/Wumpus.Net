using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class ReadState
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("mention_count")]
        public int MentionCount { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("last_message_id")]
        public Optional<Snowflake> LastMessageId { get; set; }
    }
}
