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

        public override void Validate()
        {
            base.Validate();
            Preconditions.NotNull(Topic, nameof(Topic));
            Preconditions.LengthAtLeast(Topic, Channel.MinChannelTopicLength, nameof(Topic));
            Preconditions.LengthAtMost(Topic, Channel.maxChannelTopicLength, nameof(Topic));
        }
    }
}
