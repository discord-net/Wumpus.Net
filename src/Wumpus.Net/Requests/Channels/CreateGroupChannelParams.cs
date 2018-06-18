using System.Collections.Generic;
using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class CreateGroupChannelParams
    {
        [ModelProperty("access_tokens")]
        public string[] AccessTokens { get; set; }
        [ModelProperty("nicks")]
        public Optional<Dictionary<ulong, string>> Nicks { get; set; } //TODO: Serializer does not currently support numeric keys

        public CreateGroupChannelParams(string[] accessTokens)
        {
            AccessTokens = accessTokens;
        }

        public void Validate()
        {
            Preconditions.NotNull(AccessTokens, nameof(AccessTokens));
            for (int i = 0; i < AccessTokens.Length; i++)
                Preconditions.NotNullOrWhitespace(AccessTokens[i], nameof(AccessTokens));
        }
    }
}
