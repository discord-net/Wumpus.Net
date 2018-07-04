using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#embed-object-embed-provider-structure </summary>
    public class EmbedProvider
    {
        /// <summary> Name of <see cref="EmbedProvider"/>. </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> Url of <see cref="EmbedProvider"/>. </summary>
        [ModelProperty("url")]
        public Utf8String Url { get; set; }
    }
}
