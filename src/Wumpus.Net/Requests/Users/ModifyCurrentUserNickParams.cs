using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class ModifyCurrentUserNickParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("nick")]
        public Utf8String Nickname { get; }

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
