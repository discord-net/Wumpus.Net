using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    public class CreateGuildParams
    {
        [ModelProperty("name")]
        public string Name { get; }
        [ModelProperty("region")]
        public string RegionId { get; }
        [ModelProperty("icon")]
        public Optional<Image?> Icon { get; set; }
        [ModelProperty("verification_level")]
        public Optional<VerificationLevel> VerificationLevel { get; set; }
        [ModelProperty("default_message_notifications")]
        public Optional<DefaultMessageNotifications> DefaultMessageNotifications { get; set; }
        [ModelProperty("roles")]
        public Optional<Role[]> Roles { get; set; }
        [ModelProperty("channels")]
        public Optional<Channel[]> Channels { get; set; }

        public CreateGuildParams(string name, string regionId)
        {
            Name = name;
            RegionId = regionId;
        }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Name, nameof(Name));
            Preconditions.NotNullOrWhitespace(RegionId, nameof(RegionId));
        }
    }
}
