using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class ModifyMessageParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("content")]
        public Optional<Utf8String> Content { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("embed")]
        public Optional<Embed> Embed { get; set; }

        public void Validate()
        {
            if (!Content.IsSpecified || Content.Value == (Utf8String)null)
                Content = (Utf8String)"";
            if (Embed.IsSpecified && Embed.Value != null)
                Preconditions.NotNullOrWhitespace(Content, nameof(Content));
            // else //TODO: Validate embed length
            Preconditions.LengthAtMost(Content, DiscordRestConstants.MaxMessageSize, nameof(Content));
        }
    }
}
