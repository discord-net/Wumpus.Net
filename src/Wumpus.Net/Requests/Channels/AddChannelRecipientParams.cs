using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class AddChannelRecipientParams
    {
        [ModelProperty("access_token")]
        public string AccessToken { get; set; }
        [ModelProperty("nick")]
        public Optional<string> Nickname { get; set; }

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
