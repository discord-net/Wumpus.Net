using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#attachment-object </summary>
    public class Attachment
    {
        /// <summary> <see cref="Attachment"/> id. </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> Name of the file attached. </summary>
        [ModelProperty("filename")]
        public Utf8String Filename { get; set; }
        /// <summary> Size of the file in bytes. </summary>
        [ModelProperty("size")]
        public int Size { get; set; }
        /// <summary> Source url of the file. </summary>
        [ModelProperty("url")]
        public Utf8String Url { get; set; }
        /// <summary> A proxied url of the file. </summary>
        [ModelProperty("proxy_url")]
        public Utf8String ProxyUrl { get; set; }
        /// <summary> Height of the file (if image). </summary>
        [ModelProperty("height")]
        public Optional<int> Height { get; set; }
        /// <summary> Width of the file (if image). </summary>
        [ModelProperty("width")]
        public Optional<int> Width { get; set; }
    }
}
