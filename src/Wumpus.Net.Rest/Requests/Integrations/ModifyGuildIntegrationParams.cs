using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class ModifyGuildIntegrationParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("expire_behavior")]
        public Optional<int> ExpireBehavior { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("expire_grace_period")]
        public Optional<int> ExpireGracePeriod { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("enable_emoticons")]
        public Optional<bool> EnableEmoticons { get; set; }

        public void Validate()
        {
            Preconditions.NotNegative(ExpireBehavior, nameof(ExpireBehavior));
            Preconditions.NotNegative(ExpireGracePeriod, nameof(ExpireGracePeriod));
        }
    }
}
