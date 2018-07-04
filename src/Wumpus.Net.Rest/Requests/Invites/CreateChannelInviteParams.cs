using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#create-channel-invite-json-params </summary>
    public class CreateChannelInviteParams
    {
        /// <summary> Duration of <see cref="Entities.Invite"/> in seconds before expiry, or 0 for never. </summary>
        [ModelProperty("max_age")]
        public Optional<int> MaxAge { get; set; }
        /// <summary> Max number of uses or 0 for unlimited. </summary>
        [ModelProperty("max_uses")]
        public Optional<int> MaxUses { get; set; }
        /// <summary> Whether this <see cref="Entities.Invite"/> only grants temporary membership. </summary>
        [ModelProperty("temporary")]
        public Optional<bool> IsTemporary { get; set; }
        /// <summary> If true, don't try to reuse a similar <see cref="Entities.Invite"/> (useful for creating many unique one time use <see cref="Entities.Invite"/>s) </summary>
        [ModelProperty("unique")]
        public Optional<bool> IsUnique { get; set; }

        public void Validate()
        {
            Preconditions.NotNegative(MaxAge, nameof(MaxAge));
            Preconditions.NotNegative(MaxUses, nameof(MaxUses));
        }
    }
}
