using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#create-guild-channel-json-params </summary>
    public class CreateGuildChannelParams
    {
        /// <summary> <see cref="Channel"/> name. </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; }
        /// <summary> The type of <see cref="Channel"/>. </summary>
        [ModelProperty("type")]
        public Optional<ChannelType> Type { get; }
        /// <summary> The <see cref="Channel"/>'s permission <see cref="Overwrite"/>s. </summary>
        [ModelProperty("permission_overwrites")]
        public Optional<Overwrite[]> PermissionOverwrites { get; set; }
        /// <summary> Id of the parent category for a <see cref="Channel"/>. </summary>
        [ModelProperty("parent_id")]
        public Optional<Snowflake?> ParentId { get; set; }

        public CreateGuildChannelParams(Utf8String name, ChannelType type)
        {
            Name = name;
            Type = type;
        }

        public virtual void Validate()
        {
            Preconditions.NotNullOrWhitespace(Name, nameof(Name));
            Preconditions.LengthAtLeast(Name, Channel.MinChannelNameLength, nameof(Name));
            Preconditions.LengthAtMost(Name, Channel.MaxChannelNameLength, nameof(Name));
        }
    }
}
