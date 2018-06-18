using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class EmbedProvider
    {
        [ModelProperty("name")]
        public string Name { get; set; }
        [ModelProperty("url")]
        public string Url { get; set; }
    }
}
