using System;
using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#embed-object </summary>
    public class Embed
    {
        /// <summary> Type of <see cref="Embed"/> </summary>
        /// <remarks> Always <see cref="EmbedType.Rich"/> for <see cref="Webhook"/> embeds. </remarks>
        [ModelProperty("type")]
        public EmbedType Type { get; set; } = EmbedType.Rich;
        /// <summary> Title of <see cref="Embed"/>. </summary>
        [ModelProperty("title")]
        public Optional<Utf8String> Title { get; set; }
        /// <summary> Description of <see cref="Embed"/>. </summary>
        [ModelProperty("description")]
        public Optional<Utf8String> Description { get; set; }
        /// <summary> Url of <see cref="Embed"/>. </summary>
        [ModelProperty("url")]
        public Optional<Utf8String> Url { get; set; }
        /// <summary> Timestamp of <see cref="Embed"/> content. </summary>
        [ModelProperty("timestamp"), StandardFormat('O')]
        public Optional<DateTimeOffset> Timestamp { get; set; }
        /// <summary> Color code of <see cref="Embed"/>. </summary>
        [ModelProperty("color")]
        public Optional<uint> Color { get; set; }
        /// <summary> <see cref="EmbedFooter"/> information. </summary>
        [ModelProperty("footer")]
        public Optional<EmbedFooter> Footer { get; set; }
        /// <summary> <see cref="EmbedImage"/> information. </summary>
        [ModelProperty("image")]
        public Optional<EmbedImage> Image { get; set; }
        /// <summary> <see cref="EmbedThumbnail"/> information. </summary>
        [ModelProperty("thumbnail")]
        public Optional<EmbedThumbnail> Thumbnail { get; set; }
        /// <summary> <see cref="EmbedVideo"/> information. </summary>
        [ModelProperty("video")]
        public Optional<EmbedVideo> Video { get; set; }
        /// <summary> <see cref="EmbedProvider"/> information. </summary>
        [ModelProperty("provider")]
        public Optional<EmbedProvider> Provider { get; set; }
        /// <summary> <see cref="EmbedAuthor"/> information. </summary>
        [ModelProperty("author")]
        public Optional<EmbedAuthor> Author { get; set; }
        /// <summary> <see cref="EmbedField"/> information. </summary>
        [ModelProperty("fields")]
        public Optional<EmbedField[]> Fields { get; set; }
    }
}
