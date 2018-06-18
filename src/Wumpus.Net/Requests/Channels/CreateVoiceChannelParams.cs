using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    public class CreateVoiceChannelParams : CreateGuildChannelParams
    {
        [ModelProperty("bitrate")]
        public Optional<int> Bitrate { get; set; }
        [ModelProperty("user_limit")]
        public Optional<int> UserLimit { get; set; }

        public CreateVoiceChannelParams(string name)
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
