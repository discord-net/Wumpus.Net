using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class EmbedField
    {
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public string Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("value")]
        public string Value { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("inline")]
        public Optional<bool> Inline { get; set; }
    }
}
