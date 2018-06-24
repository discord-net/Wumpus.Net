using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class EmbedProvider
    {
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("url")]
        public Utf8String Url { get; set; }
    }
}
