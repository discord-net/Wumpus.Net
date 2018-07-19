using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#modify-current-user-nick-json-params </summary>
    public class ModifyCurrentUserNickParams
    {
        /// <summary> Value to set the <see cref="Entities.User"/>'s nickname to. Requires <see cref="Entities.GuildPermissions.ChangeNickname"/>. </summary>
        [ModelProperty("nick")]
        public Utf8String Nickname { get; private set; }

        public ModifyCurrentUserNickParams(Utf8String nickname)
        {
            Nickname = nickname;
        }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Nickname, nameof(Nickname));
        }
    }
}
