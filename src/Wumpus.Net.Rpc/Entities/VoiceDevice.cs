using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class VoiceDevice
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Utf8String Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
    }
}
