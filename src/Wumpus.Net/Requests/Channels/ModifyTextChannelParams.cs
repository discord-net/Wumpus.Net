using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class ModifyTextChannelParams : ModifyGuildChannelParams
    {
        [ModelProperty("topic")]
        public Optional<string> Topic { get; set; }

        public override void Validate()
        {
            base.Validate();
            Preconditions.NotNull(Topic, nameof(Topic));
        }
    }
}
