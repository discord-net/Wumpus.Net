using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#embed-object-embed-footer-structure </summary>
    public class EmbedFooter
    {
        /// <summary> <see cref="EmbedFooter"/> text. </summary>
        [ModelProperty("text")]
        public Utf8String Text { get; set; }
        /// <summary> Url of <see cref="EmbedFooter"/> icon. </summary>
        /// <remarks> Only supports http(s) and <see cref="Attachment"/>s. </remarks>
        [ModelProperty("icon_url")]
        public Utf8String IconUrl { get; set; }
        /// <summary> A proxied url of <see cref="EmbedFooter"/> icon. </summary>
        [ModelProperty("proxy_icon_url")]
        public Utf8String ProxyIconUrl { get; set; }
    }
}
