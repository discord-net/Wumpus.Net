using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class ModifyCurrentUserNickParams
    {
        [ModelProperty("nick")]
        public string Nickname { get; }

        public ModifyCurrentUserNickParams(string nickname)
        {
            Nickname = nickname;
        }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Nickname, nameof(Nickname));
        }
    }
}
