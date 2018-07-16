using System.Collections.Generic;
using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/webhook#execute-webhook-jsonform-params </summary>
    public class ExecuteWebhookParams : QueryMap, IFormData
    {
        /// <summary> The <see cref="Message"/> contents. </summary>
        [ModelProperty("content")]
        public Optional<Utf8String> Content { get; set; }
        /// <summary> Override the default username of the <see cref="Webhook"/>. </summary>
        [ModelProperty("username")]
        public Optional<Utf8String> Username { get; set; }
        /// <summary> Override the default avatar of the <see cref="Webhook"/>. </summary>
        [ModelProperty("avatar_url")]
        public Optional<Utf8String> AvatarUrl { get; set; }
        /// <summary> True if this is a TTS <see cref="Message"/>. </summary>
        [ModelProperty("tts")]
        public Optional<bool> IsTTS { get; set; }
        /// <summary> Embedded rich content. </summary>
        [ModelProperty("embeds")]
        public Optional<Embed[]> Embeds { get; set; }

        /// <summary> Waits for server confirmation of <see cref="Message"/> send before response, and returns the created <see cref="Message"/> body. </summary>
        public Optional<bool> Wait { get; set; }
        /// <summary> The contents of the file being sent. </summary>
        public Optional<MultipartFile> File { get; set; }

        public override IDictionary<string, string> CreateQueryMap()
        {
            var map = new Dictionary<string, string>();
            if (Wait.IsSpecified)
                map["wait"] = Wait.Value.ToString();
            return map;
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
            if (!Content.IsSpecified || Content.Value == (Utf8String)null)
                Content = (Utf8String)"";
            if (Embeds.IsSpecified && Embeds.Value != null)
                Preconditions.NotNullOrWhitespace(Content, nameof(Content));
            // else //TODO: Validate embed length
            Preconditions.LengthAtMost(Content, Message.MaxContentLength, nameof(Content));
        }
    }
}
