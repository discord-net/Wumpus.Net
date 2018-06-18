using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class Reaction
    {
        [ModelProperty("count")]
        public int Count { get; set; }
        [ModelProperty("me")]
        public bool Me { get; set; }
        [ModelProperty("emoji")]
        public Emoji Emoji { get; set; }
    }
}
