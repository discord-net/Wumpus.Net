using Voltaic;
using Voltaic.Serialization;
using Wumpus.Entities;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/webhook#create-webhook-json-params </summary>
    public class CreateWebhookParams
    {
        /// <summary> Name of the <see cref="Webhook"/>. </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> Image for the default <see cref="Webhook"/> avatar. </summary>
        [ModelProperty("avatar")]
        public Image Avatar { get; set; }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Name, nameof(Name));
            Preconditions.LengthAtLeast(Name, Webhook.MinNameLength, nameof(Name));
            Preconditions.LengthAtMost(Name, Webhook.MaxNameLength, nameof(Name));
        }
    }
}
