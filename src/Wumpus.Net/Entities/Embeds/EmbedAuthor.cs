using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class EmbedAuthor
    {
        [ModelProperty("name")]
        public string Name { get; set; }
        [ModelProperty("url")]
        public string Url { get; set; }
        [ModelProperty("icon_url")]
        public string IconUrl { get; set; }
        [ModelProperty("proxy_icon_url")]
        public string ProxyIconUrl { get; set; }
    }
}
