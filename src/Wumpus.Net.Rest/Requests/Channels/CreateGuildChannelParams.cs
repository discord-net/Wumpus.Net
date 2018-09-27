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
        public Utf8String Name { get; private set; }
        /// <summary> The type of <see cref="Channel"/>. </summary>
        [ModelProperty("type")]
        public ChannelType Type { get; private set; }

        // Guild Channel

        /// <summary> Id of the parent category for a <see cref="Channel"/>. </summary>
        [ModelProperty("parent_id")]
        public Optional<Snowflake?> ParentId { get; set; }
        /// <summary> The <see cref="Channel"/>'s permission <see cref="Overwrite"/>s. </summary>
        [ModelProperty("permission_overwrites")]
        public Optional<Overwrite[]> PermissionOverwrites { get; set; }

        // Text Channel

        /// <summary> 0-1024 character <see cref="Entities.Channel"/> topic. </summary>
        [ModelProperty("topic")]
        public Optional<Utf8String> Topic { get; set; }
        /// <summary> If the <see cref="Channel"/> is nsfw. </summary>
        [ModelProperty("nsfw")]
        public Optional<bool> IsNsfw { get; set; }
        /// <summary> The amount of seconds a <see cref="User"/> has to wait before sending another <see cref="Message"/>. </summary>
        [ModelProperty("rate_limit_per_user")]
        public Optional<int> RateLimitPerUser { get; set; }

        // Voice Channel

        /// <summary> The bitrate (in bits) of the voice <see cref="Channel"/>. </summary>
        [ModelProperty("bitrate")]
        public Optional<int> Bitrate { get; set; }
        /// <summary> The <see cref="User"/> limit of the voice <see cref="Channel"/>. </summary>
        [ModelProperty("user_limit")]
        public Optional<int> UserLimit { get; set; }

        public CreateGuildChannelParams(Utf8String name, ChannelType type)
        {
            Name = name;
            Type = type;
        }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Name, nameof(Name));
            Preconditions.LengthAtLeast(Name, Channel.MinChannelNameLength, nameof(Name));
            Preconditions.LengthAtMost(Name, Channel.MaxChannelNameLength, nameof(Name));
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
