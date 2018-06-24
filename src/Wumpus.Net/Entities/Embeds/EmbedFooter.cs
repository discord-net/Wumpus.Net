using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class EmbedFooter
    {
        /// <summary> xxx </summary>
        [ModelProperty("text")]
        public string Text { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("icon_url")]
        public string IconUrl { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("proxy_icon_url")]
        public string ProxyIconUrl { get; set; }
    }
}
