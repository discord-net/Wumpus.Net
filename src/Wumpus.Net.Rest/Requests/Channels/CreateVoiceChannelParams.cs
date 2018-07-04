using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#create-guild-channel-json-params </summary>
    public class CreateVoiceChannelParams : CreateGuildChannelParams
    {
        /// <summary> The bitrate (in bits) of the voice <see cref="Channel"/>. </summary>
        [ModelProperty("bitrate")]
        public Optional<int> Bitrate { get; set; }
        /// <summary> The <see cref="User"/> limit of the voice <see cref="Channel"/>. </summary>
        [ModelProperty("user_limit")]
        public Optional<int> UserLimit { get; set; }

        public CreateVoiceChannelParams(Utf8String name)
            : base(name, ChannelType.Voice)
        {
        }

        public override void Validate()
        {
            base.Validate();
            Preconditions.Positive(Bitrate, nameof(Bitrate));
            Preconditions.AtMost(Bitrate, 128000, nameof(Bitrate));
            Preconditions.NotNegative(UserLimit, nameof(UserLimit));
        }
    }
}
