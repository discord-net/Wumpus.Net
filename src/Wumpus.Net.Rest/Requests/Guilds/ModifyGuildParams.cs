using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class ModifyGuildParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("username")]
        public Optional<Utf8String> Username { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Optional<Utf8String> Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("region")]
        public Optional<Utf8String> RegionId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("verification_level")]
        public Optional<VerificationLevel> VerificationLevel { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("default_message_notifications")]
        public Optional<DefaultMessageNotifications> DefaultMessageNotifications { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("afk_channel_id")]
        public Optional<Snowflake?> AfkChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("afk_timeout")]
        public Optional<int> AfkTimeout { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("icon")]
        public Optional<Image?> Icon { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("owner_id")]
        public Optional<Snowflake> OwnerId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("splash")]
        public Optional<Image?> Splash { get; set; }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Name, nameof(Name));
            Preconditions.NotNullOrWhitespace(RegionId, nameof(RegionId));
        }
    }
}
