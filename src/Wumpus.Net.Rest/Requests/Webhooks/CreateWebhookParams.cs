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
        public Utf8String Name { get; private set; }
        /// <summary> Image for the default <see cref="Webhook"/> avatar. </summary>
        [ModelProperty("avatar")]
        public Optional<Image> Avatar { get; set; }

        public CreateWebhookParams(Utf8String name)
        {
            Name = name;
        }

        public void Validate()
        {
            Preconditions.NotEmpty(Name, nameof(Name));
            Preconditions.LengthAtLeast(Name, Webhook.MinNameLength, nameof(Name));
            Preconditions.LengthAtMost(Name, Webhook.MaxNameLength, nameof(Name));
        }
    }
}
