using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#modify-guild-member-json-params </summary>
    public class ModifyGuildMemberParams
    {
        /// <summary> Value to set <see cref="Entities.User"/>'s nickname to. Requires <see cref="Entities.GuildPermissions.ManageNicknames"/>. </summary>
        [ModelProperty("nick")]
        public Optional<Utf8String> Nickname { get; set; }
        /// <summary> Array of <see cref="Entities.Role"/> ids the <see cref="Entities.GuildMember"/> is assigned. Requires <see cref="Entities.GuildPermissions.ManageRoles"/>. </summary>
        [ModelProperty("roles")]
        public Optional<Snowflake[]> RoleIds { get; set; }
        /// <summary> If the <see cref="Entities.User"/> is muted. Requires <see cref="Entities.GuildPermissions.MuteMembers"/>. </summary>
        [ModelProperty("mute")]
        public Optional<bool> Mute { get; set; }
        /// <summary> If the <see cref="Entities.User"/> is deafened. Requires <see cref="Entities.GuildPermissions.DeafenMembers"/>. </summary>
        [ModelProperty("deaf")]
        public Optional<bool> Deaf { get; set; }
        /// <summary> Id of the <see cref="Entities.Channel"/> to move <see cref="Entities.User"/> to (if they are connected to voice). </summary>
        [ModelProperty("channel_id")]
        public Optional<Snowflake> ChannelId { get; set; }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Nickname, nameof(Nickname));
        }
    }
}
