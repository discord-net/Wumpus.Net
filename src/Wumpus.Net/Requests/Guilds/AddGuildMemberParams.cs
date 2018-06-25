using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class AddGuildMemberParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("access_token")]
        public Utf8String AccessToken { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("nick")]
        public Optional<Utf8String> Nickname { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("roles")]
        public Optional<Role[]> Roles { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("mute")]
        public Optional<bool> Mute { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("deaf")]
        public Optional<bool> Deaf { get; set; }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(AccessToken, nameof(AccessToken));
            Preconditions.NotNullOrWhitespace(Nickname, nameof(Nickname));
        }
    }
}
