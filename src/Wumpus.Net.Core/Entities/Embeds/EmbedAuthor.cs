using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class EmbedAuthor
    {
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("url")]
        public Utf8String Url { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("icon_url")]
        public Utf8String IconUrl { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("proxy_icon_url")]
        public Utf8String ProxyIconUrl { get; set; }
    }
}
