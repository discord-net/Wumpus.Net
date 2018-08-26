using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#edit-message-json-params </summary>
    public class ModifyMessageParams
    {
        /// <summary> The new <see cref="Message"/> contents. </summary>
        [ModelProperty("content")]
        public Optional<Utf8String> Content { get; set; }
        /// <summary> Embedded rich content. </summary>
        [ModelProperty("embed")]
        public Optional<Embed> Embed { get; set; }

        public void Validate()
        {
            //TODO: Validate embed length
            Preconditions.LengthAtMost(Content, Message.MaxContentLength, nameof(Content));
        }
    }
}
