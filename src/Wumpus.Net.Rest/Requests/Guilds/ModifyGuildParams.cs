using Voltaic;
using Voltaic.Serialization;
using Wumpus.Entities;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#modify-guild-json-params </summary>
    public class ModifyGuildParams
    {
        /// <summary> <see cref="Guild"/> name. </summary>
        [ModelProperty("name")]
        public Optional<Utf8String> Name { get; set; }
        /// <summary> <see cref="Guild"/> <see cref="VoiceRegion"/> id. </summary>
        [ModelProperty("region")]
        public Optional<Utf8String> Region { get; set; }
        /// <summary> <see cref="Entities.VerificationLevel"/>. </summary>
        [ModelProperty("verification_level")]
        public Optional<VerificationLevel> VerificationLevel { get; set; }
        /// <summary> <see cref="Entities.DefaultMessageNotifications"/> level. </summary>
        [ModelProperty("default_message_notifications")]
        public Optional<DefaultMessageNotifications> DefaultMessageNotifications { get; set; }
        /// <summary> <see cref="Entities.ExplicitContentFilter"/> level. </summary>
        [ModelProperty("explicit_content_filter")]
        public Optional<ExplicitContentFilter> ExplicitContentFilter { get; set; }
        /// <summary> Id for the afk <see cref="Channel"/>. </summary>
        [ModelProperty("afk_channel_id")]
        public Optional<Snowflake?> AfkChannelId { get; set; }
        /// <summary> Afk timeout in seconds. </summary>
        [ModelProperty("afk_timeout")]
        public Optional<int> AfkTimeout { get; set; }
        /// <summary> Base64 128x128 jpeg image for the <see cref="Guild"/> icon. </summary>
        [ModelProperty("icon")]
        public Optional<Image?> Icon { get; set; }
        /// <summary> <see cref="User"/> id to transfer <see cref="Guild"/> ownership to. (Must be owner). </summary>
        [ModelProperty("owner_id")]
        public Optional<Snowflake> OwnerId { get; set; }
        /// <summary> Base64 128x128 jpeg image for the <see cref="Guild"/> splash. (VIP only) </summary>
        [ModelProperty("splash")]
        public Optional<Image?> Splash { get; set; }
        /// <summary> The id of the <see cref="Channel"/> to which system <see cref="Message"/>s are sent. </summary>
        [ModelProperty("system_channel_id")]
        public Optional<Snowflake?> SystemChannelId { get; set; }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Name, nameof(Name));
            Preconditions.LengthAtLeast(Name, Guild.MinNameLength, nameof(Name));
            Preconditions.LengthAtMost(Name, Guild.MaxNameLength, nameof(Name));

            Preconditions.NotNullOrWhitespace(Region, nameof(Region));

            Preconditions.AtLeast(AfkTimeout, Channel.MinAfkTimeoutDuration, nameof(AfkTimeout));
            Preconditions.AtMost(AfkTimeout, Channel.MaxAFkTimeoutDuration, nameof(AfkTimeout));
        }
    }
}
