using Voltaic;
using Voltaic.Serialization;
using Wumpus.Entities;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#create-guild-channel-json-params </summary>
    public class CreateTextChannelParams : CreateGuildChannelParams
    {
        /// <summary> If the <see cref="Channel"/> is nsfw. </summary>
        [ModelProperty("nsfw")]
        public Optional<bool> IsNsfw { get; set; }

        public CreateTextChannelParams(Utf8String name)
            : base(name, ChannelType.Text)
        {
        }
    }
}
