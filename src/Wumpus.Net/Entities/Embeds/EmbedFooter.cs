using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class EmbedFooter
    {
        [ModelProperty("text")]
        public string Text { get; set; }
        [ModelProperty("icon_url")]
        public string IconUrl { get; set; }
        [ModelProperty("proxy_icon_url")]
        public string ProxyIconUrl { get; set; }
    }
}
