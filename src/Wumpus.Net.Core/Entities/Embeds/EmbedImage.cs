using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#embed-object-embed-image-structure </summary>
    public class EmbedImage
    {
        /// <summary> Source url of <see cref="EmbedImage"/>. </summary>
        /// <remarks> Only supports http(s) and <see cref="Attachment"/>s. </remarks>
        [ModelProperty("url")]
        public Utf8String Url { get; set; }
        /// <summary> A proxied url of <see cref="EmbedImage"/>. </summary>
        [ModelProperty("proxy_url")]
        public Utf8String ProxyUrl { get; set; }
        /// <summary> Height of <see cref="EmbedImage"/>. </summary>
        [ModelProperty("height")]
        public Optional<int> Height { get; set; }
        /// <summary> Width of <see cref="EmbedImage"/>. </summary>
        [ModelProperty("width")]
        public Optional<int> Width { get; set; }
    }
}
