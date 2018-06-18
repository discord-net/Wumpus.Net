using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class Game
    {
        [ModelProperty("name")]
        public string Name { get; set; }
        [ModelProperty("url")]
        public Optional<string> StreamUrl { get; set; }
        [ModelProperty("type")]
        public Optional<StreamType?> StreamType { get; set; }
    }
}
