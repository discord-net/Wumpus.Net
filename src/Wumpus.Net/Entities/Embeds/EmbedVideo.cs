using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class EmbedVideo
    {
        [ModelProperty("url")]
        public string Url { get; set; }
        [ModelProperty("height")]
        public Optional<int> Height { get; set; }
        [ModelProperty("width")]
        public Optional<int> Width { get; set; }
    }
}
