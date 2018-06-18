using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class ModifyGuildIntegrationParams
    {
        [ModelProperty("expire_behavior")]
        public Optional<int> ExpireBehavior { get; set; }
        [ModelProperty("expire_grace_period")]
        public Optional<int> ExpireGracePeriod { get; set; }
        [ModelProperty("enable_emoticons")]
        public Optional<bool> EnableEmoticons { get; set; }

        public void Validate()
        {
            Preconditions.NotNegative(ExpireBehavior, nameof(ExpireBehavior));
            Preconditions.NotNegative(ExpireGracePeriod, nameof(ExpireGracePeriod));
        }
    }
}
