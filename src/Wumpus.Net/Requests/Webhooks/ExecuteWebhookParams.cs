using System.Collections.Generic;
using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    public class ExecuteWebhookParams : QueryMap, IFormData
    {
        [ModelProperty("content")]
        public Optional<string> Content { get; set; }
        [ModelProperty("nonce")]
        public Optional<string> Nonce { get; set; }
        [ModelProperty("tts")]
        public Optional<bool> IsTTS { get; set; }
        [ModelProperty("embeds")]
        public Optional<Embed[]> Embeds { get; set; }

        [ModelProperty("username")]
        public Optional<string> Username { get; set; }
        [ModelProperty("avatar_url")]
        public Optional<string> AvatarUrl { get; set; }

        public Optional<bool> Wait { get; set; }
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
