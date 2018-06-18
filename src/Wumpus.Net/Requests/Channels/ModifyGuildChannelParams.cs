using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class ModifyGuildChannelParams
    {
        [ModelProperty("name")]
        public Optional<string> Name { get; set; }
        [ModelProperty("position")]
        public Optional<int> Position { get; set; }

        public virtual void Validate()
        {
            Preconditions.NotNullOrWhitespace(Name, nameof(Name));
            Preconditions.NotNegative(Position, nameof(Position));
        }
    }
}
