using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class EmbedVideo
    {
        /// <summary> xxx </summary>
        [ModelProperty("url")]
        public string Url { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("height")]
        public Optional<int> Height { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("width")]
        public Optional<int> Width { get; set; }
    }
}
