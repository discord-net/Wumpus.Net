using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#add-guild-member-json-params </summary>
    public class AddGuildMemberParams
    {
        /// <summary> An OAuth2 access token granted with the <see cref="DiscordOAuthScope.GuildsJoin"/> scope to the bot's <see cref="Application"/> for the <see cref="User"/> you want ot add to the <see cref="Guild"/>. </summary>
        [ModelProperty("access_token")]
        public Utf8String AccessToken { get; set; }
        /// <summary> Value to set the <see cref="User"/>'s nickname to. Requires <see cref="GuildPermissions.ManageNicknames"/>. </summary>
        [ModelProperty("nick")]
        public Optional<Utf8String> Nickname { get; set; }
        /// <summary> Array of <see cref="Role"/> ids the member is assigned. Requires <see cref="GuildPermissions.ManageRoles"/>. </summary>
        [ModelProperty("roles")]
        public Optional<Snowflake[]> Roles { get; set; }
        /// <summary> If the <see cref="User"/> is muted. Requires <see cref="GuildPermissions.MuteMembers"/>. </summary>
        [ModelProperty("mute")]
        public Optional<bool> Mute { get; set; }
        /// <summary> If the <see cref="User"/ >is deafened. Requires <see cref="GuildPermissions.DeafenMembers"/>. </summary>
        [ModelProperty("deaf")]
        public Optional<bool> Deaf { get; set; }

        public AddGuildMemberParams(Utf8String accessToken)
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
