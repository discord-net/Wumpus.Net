using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class ModifyGuildMemberParams
    {
        [ModelProperty("mute")]
        public Optional<bool> Mute { get; set; }
        [ModelProperty("deaf")]
        public Optional<bool> Deaf { get; set; }
        [ModelProperty("nick")]
        public Optional<string> Nickname { get; set; }
        [ModelProperty("roles")]
        public Optional<ulong[]> RoleIds { get; set; }
        [ModelProperty("channel_id")]
        public Optional<ulong> ChannelId { get; set; }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Nickname, nameof(Nickname));
        }
    }
}
