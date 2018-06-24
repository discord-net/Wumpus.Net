using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class CreateChannelInviteParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("max_age")]
        public Optional<int> MaxAge { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("max_uses")]
        public Optional<int> MaxUses { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("temporary")]
        public Optional<bool> IsTemporary { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("unique")]
        public Optional<bool> IsUnique { get; set; }

        public void Validate()
        {
            Preconditions.NotNegative(MaxAge, nameof(MaxAge));
            Preconditions.NotNegative(MaxUses, nameof(MaxUses));
        }
    }
}
