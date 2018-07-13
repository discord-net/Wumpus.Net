using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#modify-guild-integration-json-params </summary>
    public class ModifyGuildIntegrationParams
    {
        /// <summary> The behavior when an <see cref="Entities.Integration"/> subscription lapses. </summary>
        [ModelProperty("expire_behavior")]
        public Optional<int> ExpireBehavior { get; set; }
        /// <summary> Period (in seconds) where the <see cref="Entities.Integration"/> will ignore lapsed subscriptions. </summary>
        [ModelProperty("expire_grace_period")]
        public Optional<int> ExpireGracePeriod { get; set; }
        /// <summary> Whether emoticons should be synced for this <see cref="Entities.Integration"/>. </summary>
        [ModelProperty("enable_emoticons")]
        public Optional<bool> EnableEmoticons { get; set; }

        public void Validate()
        {
            Preconditions.NotNegative(ExpireBehavior, nameof(ExpireBehavior));
            Preconditions.NotNegative(ExpireGracePeriod, nameof(ExpireGracePeriod));
        }
    }
}
