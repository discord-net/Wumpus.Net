using Voltaic;
using Voltaic.Serialization;
using Wumpus.Entities;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#modify-channel-json-params </summary>
    public class ModifyTextChannelParams : ModifyGuildChannelParams
    {
        /// <summary> 0-1024 character <see cref="Entities.Channel"/> topic. </summary>
        [ModelProperty("topic")]
        public Optional<Utf8String> Topic { get; set; }
        /// <summary> If the <see cref="Entities.Channel"/> is nsfw. </summary>
        [ModelProperty("nsfw")]
        public Optional<bool> Nsfw { get; set; }
        /// <summary> Id of the new parent category for a <see cref="Channel"/>. </summary>
        [ModelProperty("parent_id")]
        public Optional<Snowflake?> ParentId { get; set; }

        public override void Validate()
        {
            base.Validate();
            Preconditions.NotNull(Topic, nameof(Topic));
            Preconditions.LengthAtLeast(Topic, Channel.MinChannelTopicLength, nameof(Topic));
            Preconditions.LengthAtMost(Topic, Channel.MaxChannelTopicLength, nameof(Topic));
        }
    }
}
