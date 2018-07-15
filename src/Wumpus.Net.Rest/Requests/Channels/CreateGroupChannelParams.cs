using System.Collections.Generic;
using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/user#create-group-dm-json-params </summary>
    public class CreateGroupDMChannelParams
    {
        /// <summary> Access tokens of <see cref="Entities.User"/>s that have granted your app the <see cref="Entities.DiscordOAuthScope.GroupDMJoin"/> scope. </summary>
        [ModelProperty("access_tokens")]
        public Utf8String[] AccessTokens { get; set; }
        /// <summary> A dictionary of <see cref="Entities.User"/> ids to their respective nicknames. </summary>
        [ModelProperty("nicks")]
        public Optional<Dictionary<Snowflake, string>> Nicks { get; set; } //TODO: Serializer does not currently support numeric keys

        public CreateGroupDMChannelParams(Utf8String[] accessTokens)
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
