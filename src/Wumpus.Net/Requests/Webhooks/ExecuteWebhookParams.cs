using System.Collections.Generic;
using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class ExecuteWebhookParams : QueryMap, IFormData
    {
        /// <summary> xxx </summary>
        [ModelProperty("content")]
        public Optional<Utf8String> Content { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("nonce")]
        public Optional<Utf8String> Nonce { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("tts")]
        public Optional<bool> IsTTS { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("embeds")]
        public Optional<Embed[]> Embeds { get; set; }

        /// <summary> xxx </summary>
        [ModelProperty("username")]
        public Optional<Utf8String> Username { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("avatar_url")]
        public Optional<Utf8String> AvatarUrl { get; set; }

        /// <summary> xxx </summary>
        public Optional<bool> Wait { get; set; }
        /// <summary> xxx </summary>
        public Optional<MultipartFile> File { get; set; }

        public override IDictionary<string, object> GetQueryMap()
        {
            var dict = new Dictionary<string, object>();
            if (Wait.IsSpecified)
                dict["wait"] = Wait.Value;
            return dict;
        }

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

            if (!Content.IsSpecified || Content.Value == null)
                Content = "";
            if (Embeds.IsSpecified && Embeds.Value != null)
                Preconditions.NotNullOrWhitespace(Content, nameof(Content));
            // else //TODO: Validate embed length
            Preconditions.LengthAtMost(Content, DiscordConstants.MaxMessageSize, nameof(Content));
        }
    }
}
