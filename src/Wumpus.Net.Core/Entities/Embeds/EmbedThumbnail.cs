using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#embed-object-embed-thumbnail-structure </summary>
    public class EmbedThumbnail
    {
        /// <summary> Source url of <see cref="EmbedThumbnail"/>. </summary>
        /// <remarks> Only supports http(s) and <see cref="Attachment"/>s. </remarks>
        [ModelProperty("url")]
        public Utf8String Url { get; set; }
        /// <summary> A proxied url of the <see cref="EmbedThumbnail"/>. </summary>
        [ModelProperty("proxy_url")]
        public Utf8String ProxyUrl { get; set; }
        /// <summary> Height of <see cref="EmbedThumbnail"/>. </summary>
        [ModelProperty("height")]
        public Optional<int> Height { get; set; }
        /// <summary> Width of <see cref="EmbedThumbnail"/>. </summary>
        [ModelProperty("width")]
        public Optional<int> Width { get; set; }
    }
}
