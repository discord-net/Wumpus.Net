using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#embed-object-embed-author-structure </summary>
    public class EmbedAuthor
    {
        /// <summary> Name of <see cref="EmbedAuthor"/>. </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> Url of <see cref="EmbedAuthor"/>. </summary>
        [ModelProperty("url")]
        public Utf8String Url { get; set; }
        /// <summary> Url of <see cref="EmbedAuthor"/> icon. </summary>
        /// <remarks> Only supports http(s) and <see cref="Attachment"/>s. </remarks>
        [ModelProperty("icon_url")]
        public Utf8String IconUrl { get; set; }
        /// <summary> A proxied url of <see cref="EmbedAuthor"/> icon. </summary>
        [ModelProperty("proxy_icon_url")]
        public Utf8String ProxyIconUrl { get; set; }
    }
}
