using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#embed-object-embed-field-structure </summary>
    public class EmbedField
    {
        /// <summary> Name of the <see cref="EmbedField"/>. </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> Value of the <see cref="EmbedField"/>. </summary>
        [ModelProperty("value")]
        public Utf8String Value { get; set; }
        /// <summary> Whether or not this <see cref="EmbedField"/> should display inline. </summary>
        [ModelProperty("inline")]
        public Optional<bool> Inline { get; set; }
    }
}
