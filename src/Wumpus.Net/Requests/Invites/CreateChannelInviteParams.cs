using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class CreateChannelInviteParams
    {
        [ModelProperty("max_age")]
        public Optional<int> MaxAge { get; set; }
        [ModelProperty("max_uses")]
        public Optional<int> MaxUses { get; set; }
        [ModelProperty("temporary")]
        public Optional<bool> IsTemporary { get; set; }
        [ModelProperty("unique")]
        public Optional<bool> IsUnique { get; set; }

        public void Validate()
        {
            Preconditions.NotNegative(MaxAge, nameof(MaxAge));
            Preconditions.NotNegative(MaxUses, nameof(MaxUses));
        }
    }
}
