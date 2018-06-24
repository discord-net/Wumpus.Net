using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class Attachment
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("filename")]
        public Utf8String Filename { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("size")]
        public int Size { get; set; }
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
