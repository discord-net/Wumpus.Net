using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#group-dm-add-recipient-json-params </summary>
    public class AddChannelRecipientParams
    {
        /// <summary> Access token of a <see cref="Entities.User"/> that has granted your app the <see cref="Entities.DiscordOAuthScope.GroupDMJoin"/> scope. </summary>
        [ModelProperty("access_token")]
        public Utf8String AccessToken { get; private set; }
        /// <summary> Nickname of the <see cref="Entities.User"/> being added. </summary>
        [ModelProperty("nick")]
        public Optional<Utf8String> Nickname { get; set; }

        public AddChannelRecipientParams(Utf8String accessToken)
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
