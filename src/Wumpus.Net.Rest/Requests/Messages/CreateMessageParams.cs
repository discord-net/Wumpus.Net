using System.Collections.Generic;
using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    /// <summary> 
    /// Post a message to a <see cref="Guild"/> text or DM <see cref="Channel"/>. If operating on a <see cref="Guild"/> <see cref="Channel"/>, this endpoint requires <see cref="ChannelPermissions.SendMessages"/> to be present on the current <see cref="User"/>.
    /// If the tts field is set to true, <see cref="ChannelPermissions.SendTTSMessages"/> is required for the message to be spoken. 
    /// Returns a <see cref="Message"/> object. 
    /// Fires a Message Create Gateway event. 
    /// </summary>
    public class CreateMessageParams : IFormData
    {
        /// <summary> The <see cref="Message"/> contents. </summary>
        /// <remarks> Up to 2000 characters. </remarks>
        [ModelProperty("content")]
        public Optional<Utf8String> Content { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("nonce")]
        public Optional<Utf8String> Nonce { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("tts")]
        public Optional<bool> IsTTS { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("embed")]
        public Optional<Embed> Embed { get; set; }

        public Optional<MultipartFile> File { get; set; }
        
        public IDictionary<string, object> GetFormData()
        {
            var dict = new Dictionary<string, object>();
            if (File.IsSpecified)
                dict["file"] = File.Value;
            dict["payload_json"] = this;
            return dict;
        }

        public void Validate()
        {
            Preconditions.NotNull(Nonce, nameof(Nonce));

            if (!Content.IsSpecified || Content.Value == (Utf8String)null)
                Content = (Utf8String)"";
            if (Embed.IsSpecified && Embed.Value != null)
                Preconditions.NotNullOrWhitespace(Content, nameof(Content));
            // else //TODO: Validate embed length
            Preconditions.LengthAtMost(Content, DiscordRestConstants.MaxMessageSize, nameof(Content));
        }
    }
}
