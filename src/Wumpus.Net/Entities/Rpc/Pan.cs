using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class Pan
    {
        [ModelProperty("left")]
        public float Left { get; set; }
        [ModelProperty("right")]
        public float Right { get; set; }
    }
}
