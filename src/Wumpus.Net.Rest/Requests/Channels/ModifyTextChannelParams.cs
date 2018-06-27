using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class ModifyTextChannelParams : ModifyGuildChannelParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("topic")]
        public Optional<Utf8String> Topic { get; set; }

        public override void Validate()
        {
            base.Validate();
            Preconditions.NotNull(Topic, nameof(Topic));
        }
    }
}
