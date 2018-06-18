using System;
using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class Embed
    {
        [ModelProperty("type")]
        public EmbedType Type { get; set; } = EmbedType.Rich;
        [ModelProperty("title")]
        public Optional<string> Title { get; set; }
        [ModelProperty("description")]
        public Optional<string> Description { get; set; }
        [ModelProperty("url")]
        public Optional<string> Url { get; set; }
        [ModelProperty("timestamp")]
        public Optional<DateTimeOffset> Timestamp { get; set; }
        [ModelProperty("color")]
        public Optional<uint> Color { get; set; }
        [ModelProperty("footer")]
        public Optional<EmbedFooter> Footer { get; set; }
        [ModelProperty("image")]
        public Optional<EmbedImage> Image { get; set; }
        [ModelProperty("thumbnail")]
        public Optional<EmbedThumbnail> Thumbnail { get; set; }
        [ModelProperty("video")]
        public Optional<EmbedVideo> Video { get; set; }
        [ModelProperty("provider")]
        public Optional<EmbedProvider> Provider { get; set; }
        [ModelProperty("author")]
        public Optional<EmbedAuthor> Author { get; set; }
        [ModelProperty("fields")]
        public Optional<EmbedField[]> Fields { get; set; }
    }
}
