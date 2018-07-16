using Voltaic;
using Voltaic.Serialization;
using Wumpus.Entities;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/webhook#modify-webhook-json-params </summary>
    public class ModifyWebhookParams
    {
        /// <summary> The default name of the <see cref="Webhook"/>. </summary>
        [ModelProperty("name")]
        public Optional<Utf8String> Name { get; set; }
        /// <summary> Image for the default <see cref="Webhook"/> avatar. </summary>
        [ModelProperty("avatar")]
        public Optional<Image?> Avatar { get; set; }
        /// <summary> The new <see cref="Channel"/> id this <see cref="Webhook"/> should be moved to. </summary>
        [ModelProperty("channel_id")]
        public Optional<Snowflake> ChannelId { get; set; }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Name, nameof(Name));
            Preconditions.LengthAtLeast(Name, Webhook.MinNameLength, nameof(Name));
            Preconditions.LengthAtMost(Name, Webhook.MaxNameLength, nameof(Name));
        }
    }
}
