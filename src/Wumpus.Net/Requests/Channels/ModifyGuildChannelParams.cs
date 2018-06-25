using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class ModifyGuildChannelParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Optional<Utf8String> Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("position")]
        public Optional<int> Position { get; set; }

        public virtual void Validate()
        {
            Preconditions.NotNullOrWhitespace(Name, nameof(Name));
            Preconditions.NotNegative(Position, nameof(Position));
        }
    }
}
