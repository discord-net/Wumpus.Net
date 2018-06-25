using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class AddChannelRecipientParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("access_token")]
        public Utf8String AccessToken { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("nick")]
        public Optional<Utf8String> Nickname { get; set; }

        public AddChannelRecipientParams(string accessToken)
        {
            AccessToken = accessToken;
        }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(AccessToken, nameof(AccessToken));
            Preconditions.NotNullOrWhitespace(Nickname, nameof(Nickname));
        }
    }
}
