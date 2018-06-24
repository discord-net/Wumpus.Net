using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class EmbedThumbnail
    {
        /// <summary> xxx </summary>
        [ModelProperty("url")]
        public Utf8String Url { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("proxy_url")]
        public Utf8String ProxyUrl { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("height")]
        public Optional<int> Height { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("width")]
        public Optional<int> Width { get; set; }
    }
}
