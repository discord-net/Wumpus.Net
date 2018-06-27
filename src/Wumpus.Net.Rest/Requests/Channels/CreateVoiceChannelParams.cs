using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class CreateVoiceChannelParams : CreateGuildChannelParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("bitrate")]
        public Optional<int> Bitrate { get; set; }
        /// <summary> xxx </summary>
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
