using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class EmbedField
    {
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("value")]
        public Utf8String Value { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("inline")]
        public Optional<bool> Inline { get; set; }
    }
}
