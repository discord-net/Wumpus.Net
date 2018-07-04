using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#embed-object-embed-video-structure </summary>
    public class EmbedVideo
    {
        /// <summary> Source url of <see cref="EmbedVideo"/>. </summary>
        [ModelProperty("url")]
        public Utf8String Url { get; set; }
        /// <summary> Height of <see cref="EmbedVideo"/>. </summary>
        [ModelProperty("height")]
        public Optional<int> Height { get; set; }
        /// <summary> Width of <see cref="EmbedVideo"/>. </summary>
        [ModelProperty("width")]
        public Optional<int> Width { get; set; }
    }
}
