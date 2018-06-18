using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    public class ModifyGuildParams
    {
        [ModelProperty("username")]
        public Optional<string> Username { get; set; }
        [ModelProperty("name")]
        public Optional<string> Name { get; set; }
        [ModelProperty("region")]
        public Optional<string> RegionId { get; set; }
        [ModelProperty("verification_level")]
        public Optional<VerificationLevel> VerificationLevel { get; set; }
        [ModelProperty("default_message_notifications")]
        public Optional<DefaultMessageNotifications> DefaultMessageNotifications { get; set; }
        [ModelProperty("afk_channel_id")]
        public Optional<ulong?> AfkChannelId { get; set; }
        [ModelProperty("afk_timeout")]
        public Optional<int> AfkTimeout { get; set; }
        [ModelProperty("icon")]
        public Optional<Image?> Icon { get; set; }
        [ModelProperty("owner_id")]
        public Optional<ulong> OwnerId { get; set; }
        [ModelProperty("splash")]
        public Optional<Image?> Splash { get; set; }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Name, nameof(Name));
            Preconditions.NotNullOrWhitespace(RegionId, nameof(RegionId));
        }
    }
}
