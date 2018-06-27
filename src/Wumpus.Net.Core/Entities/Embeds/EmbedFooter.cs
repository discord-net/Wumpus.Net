using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class EmbedFooter
    {
        /// <summary> xxx </summary>
        [ModelProperty("text")]
        public Utf8String Text { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("icon_url")]
        public Utf8String IconUrl { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("proxy_icon_url")]
        public Utf8String ProxyIconUrl { get; set; }
    }
}
