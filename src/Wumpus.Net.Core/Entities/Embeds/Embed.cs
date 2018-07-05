using System;
using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class Embed
    {
        /// <summary> xxx </summary>
        [ModelProperty("type")]
        public EmbedType Type { get; set; } = EmbedType.Rich;
        /// <summary> xxx </summary>
        [ModelProperty("title")]
        public Optional<Utf8String> Title { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("description")]
        public Optional<Utf8String> Description { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("url")]
        public Optional<Utf8String> Url { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("timestamp"), StandardFormat('O')]
        public Optional<DateTimeOffset> Timestamp { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("color")]
        public Optional<uint> Color { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("footer")]
        public Optional<EmbedFooter> Footer { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("image")]
        public Optional<EmbedImage> Image { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("thumbnail")]
        public Optional<EmbedThumbnail> Thumbnail { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("video")]
        public Optional<EmbedVideo> Video { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("provider")]
        public Optional<EmbedProvider> Provider { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("author")]
        public Optional<EmbedAuthor> Author { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("fields")]
        public Optional<EmbedField[]> Fields { get; set; }
    }
}
