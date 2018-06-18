using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class ModifyVoiceChannelParams : ModifyGuildChannelParams
    {
        [ModelProperty("bitrate")]
        public Optional<int> Bitrate { get; set; }
        [ModelProperty("user_limit")]
        public Optional<int> UserLimit { get; set; }

        public override void Validate()
        {
            base.Validate();
            Preconditions.Positive(Bitrate, nameof(Bitrate));
            Preconditions.AtMost(Bitrate, 128000, nameof(Bitrate));
            Preconditions.NotNegative(UserLimit, nameof(UserLimit));
        }
    }
}
