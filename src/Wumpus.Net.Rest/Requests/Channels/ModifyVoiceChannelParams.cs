using Voltaic;
using Voltaic.Serialization;
using Wumpus.Entities;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#modify-channel-json-params </summary>
    public class ModifyVoiceChannelParams : ModifyGuildChannelParams
    {
        /// <summary> The bitrate (in bits) of the voice <see cref="Channel"/>. </summary>
        [ModelProperty("bitrate")]
        public Optional<int> Bitrate { get; set; }
        /// <summary> The <see cref="User"/> limit of the voice <see cref="Channel"/>. </summary>
        [ModelProperty("user_limit")]
        public Optional<int> UserLimit { get; set; }
        /// <summary> Id of the new parent category for a <see cref="Channel"/>. </summary>
        [ModelProperty("parent_id")]
        public Optional<Snowflake?> ParentId { get; set; }

        public override void Validate()
        {
            base.Validate();
            Preconditions.AtLeast(Bitrate, Channel.MinBitrate, nameof(Bitrate));
            Preconditions.AtMost(Bitrate, Channel.MaxBitrate, nameof(Bitrate));

            Preconditions.AtLeast(UserLimit, Channel.MinUserLimit, nameof(UserLimit));
            Preconditions.AtMost(UserLimit, Channel.MaxUserLimit, nameof(UserLimit));
        }
    }
}
