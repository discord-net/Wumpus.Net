using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class ReadState
    {
        [ModelProperty("id")]
        public ulong Id { get; set; }
        [ModelProperty("mention_count")]
        public int MentionCount { get; set; }
        [ModelProperty("last_message_id")]
        public Optional<ulong> LastMessageId { get; set; }
    }
}
