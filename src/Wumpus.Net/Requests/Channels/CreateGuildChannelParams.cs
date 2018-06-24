using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class CreateGuildChannelParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public string Name { get; }
        /// <summary> xxx </summary>
        [ModelProperty("type")]
        public ChannelType Type { get; }
        /// <summary> xxx </summary>
        [ModelProperty("permission_overwrites")]
        public Optional<Overwrite[]> PermissionOverwrites { get; set; }

        public CreateGuildChannelParams(string name, ChannelType type)
        {
            Name = name;
            Type = type;
        }

        public virtual void Validate()
        {
            Preconditions.NotNullOrWhitespace(Name, nameof(Name));
        }
    }
}
