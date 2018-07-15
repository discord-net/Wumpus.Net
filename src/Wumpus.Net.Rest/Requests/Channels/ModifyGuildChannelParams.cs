using Voltaic;
using Voltaic.Serialization;
using Wumpus.Entities;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#modify-channel-json-params </summary>
    public class ModifyGuildChannelParams
    {
        /// <summary> 2-100 character <see cref="Channel"/> name. </summary>
        [ModelProperty("name")]
        public Optional<Utf8String> Name { get; set; }
        /// <summary> The position of the <see cref="Channel"/> in the left-hand listing. </summary>
        [ModelProperty("position")]
        public Optional<int> Position { get; set; }
        /// <summary> <see cref="Channel"/> or category-specific <see cref="Overwrite"/>s. </summary>
        [ModelProperty("permission_overwrites")]
        public Optional<Overwrite[]> PermissionOverwrites { get; set; }

        public virtual void Validate()
        {
            Preconditions.NotNullOrWhitespace(Name, nameof(Name));
            Preconditions.LengthAtLeast(Name, Channel.MinChannelNameLength, nameof(Name));
            Preconditions.LengthAtMost(Name, Channel.MaxChannelNameLength, nameof(Name));

            Preconditions.NotNegative(Position, nameof(Position));
        }
    }
}
