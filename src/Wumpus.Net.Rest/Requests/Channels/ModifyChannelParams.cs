using Voltaic;
using Voltaic.Serialization;
using Wumpus.Entities;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#modify-channel-json-params </summary>
    public class ModifyChannelParams
    {
        /// <summary> 2-100 character <see cref="Channel"/> name. </summary>
        [ModelProperty("name")]
        public Optional<Utf8String> Name { get; set; }

        // Guild Channel

        /// <summary> Id of the new parent category for a <see cref="Channel"/>. </summary>
        [ModelProperty("parent_id")]
        public Optional<Snowflake?> ParentId { get; set; }
        /// <summary> <see cref="Channel"/> or category-specific <see cref="Overwrite"/>s. </summary>
        [ModelProperty("permission_overwrites")]
        public Optional<Overwrite[]> PermissionOverwrites { get; set; }
        /// <summary> The position of the <see cref="Channel"/> in the left-hand listing. </summary>
        [ModelProperty("position")]
        public Optional<int> Position { get; set; }

        // Text Channel

        /// <summary> 0-1024 character <see cref="Entities.Channel"/> topic. </summary>
        [ModelProperty("topic")]
        public Optional<Utf8String> Topic { get; set; }
        /// <summary> If the <see cref="Entities.Channel"/> is nsfw. </summary>
        [ModelProperty("nsfw")]
        public Optional<bool> IsNsfw { get; set; }

        // Voice Channel

        /// <summary> The bitrate (in bits) of the voice <see cref="Channel"/>. </summary>
        [ModelProperty("bitrate")]
        public Optional<int> Bitrate { get; set; }
        /// <summary> The <see cref="User"/> limit of the voice <see cref="Channel"/>. </summary>
        [ModelProperty("user_limit")]
        public Optional<int> UserLimit { get; set; }

        public virtual void Validate()
        {
            Preconditions.NotNullOrWhitespace(Name, nameof(Name));
            Preconditions.LengthAtLeast(Name, Channel.MinChannelNameLength, nameof(Name));
            Preconditions.LengthAtMost(Name, Channel.MaxChannelNameLength, nameof(Name));
            Preconditions.NotNegative(Position, nameof(Position));
            Preconditions.NotNull(PermissionOverwrites, nameof(PermissionOverwrites));
            Preconditions.NotNull(Topic, nameof(Topic));
            Preconditions.LengthAtLeast(Topic, Channel.MinChannelTopicLength, nameof(Topic));
            Preconditions.LengthAtMost(Topic, Channel.MaxChannelTopicLength, nameof(Topic));
            Preconditions.AtLeast(Bitrate, Channel.MinBitrate, nameof(Bitrate));
            Preconditions.AtMost(Bitrate, Channel.MaxBitrate, nameof(Bitrate));
            Preconditions.AtLeast(UserLimit, Channel.MinUserLimit, nameof(UserLimit));
            Preconditions.AtMost(UserLimit, Channel.MaxUserLimit, nameof(UserLimit));
        }
    }
}
