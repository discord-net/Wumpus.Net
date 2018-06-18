using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    public class AddGuildMemberParams
    {
        [ModelProperty("access_token")]
        public string AccessToken { get; set; }
        [ModelProperty("nick")]
        public Optional<string> Nickname { get; set; }
        [ModelProperty("roles")]
        public Optional<Role[]> Roles { get; set; }
        [ModelProperty("mute")]
        public Optional<bool> Mute { get; set; }
        [ModelProperty("deaf")]
        public Optional<bool> Deaf { get; set; }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(AccessToken, nameof(AccessToken));
            Preconditions.NotNullOrWhitespace(Nickname, nameof(Nickname));
        }
    }
}
