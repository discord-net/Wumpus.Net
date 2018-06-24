using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class EmbedAuthor
    {
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public string Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("url")]
        public string Url { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("icon_url")]
        public string IconUrl { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("proxy_icon_url")]
        public string ProxyIconUrl { get; set; }
    }
}
