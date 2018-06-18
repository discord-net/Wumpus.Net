using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class CreateWebhookParams
    {
        [ModelProperty("name")]
        public string Name { get; set; }
        [ModelProperty("avatar")]
        public Image Avatar { get; set; }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Name, nameof(Name));
        }
    }
}
