using System.Collections.Generic;
using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/user#create-dm-json-params </summary>
    public class CreateDMChannelParams
    {
        // DM Channel
        /// <summary> The recipient to open a DM <see cref="Entities.Channel"/> with. </summary>
        [ModelProperty("recipient_id")]
        public Optional<Snowflake> RecipientId { get; private set; }

        // Group DM Channel

        /// <summary> Access tokens of <see cref="Entities.User"/>s that have granted your app the <see cref="Entities.DiscordOAuthScope.GroupDMJoin"/> scope. </summary>
        [ModelProperty("access_tokens")]
        public Optional<Utf8String[]> AccessTokens { get; private set; }
        /// <summary> A dictionary of <see cref="Entities.User"/> ids to their respective nicknames. </summary>
        [ModelProperty("nicks")]
        public Optional<Dictionary<Snowflake, string>> Nicks { get; set; } //TODO: Serializer does not currently support numeric keys
        
        public CreateDMChannelParams(Snowflake recipientId)
        {
            RecipientId = recipientId;
        }
        public CreateDMChannelParams(Utf8String[] accessTokens)
        {
            AccessTokens = accessTokens;
        }

        public void Validate()
        {
            Preconditions.NotNull(AccessTokens, nameof(AccessTokens));
            if (AccessTokens.IsSpecified)
            {
                for (int i = 0; i < AccessTokens.Value.Length; i++)
                    Preconditions.NotNullOrWhitespace(AccessTokens.Value[i], nameof(AccessTokens));
            }
            Preconditions.NotNull(Nicks, nameof(Nicks));
        }
    }
}
