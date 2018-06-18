using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class EmbedField
    {
        [ModelProperty("name")]
        public string Name { get; set; }
        [ModelProperty("value")]
        public string Value { get; set; }
        [ModelProperty("inline")]
        public Optional<bool> Inline { get; set; }
    }
}
