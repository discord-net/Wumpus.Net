using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#create-guild-role-json-params </summary>
    public class CreateGuildRoleParams
    {
        /// <summary> Name of the <see cref="Role"/>. </summary>
        [ModelProperty("name")]
        public Optional<Utf8String> Name { get; set; }
        /// <summary> Bitwise of the enabled/disabled permissions. </summary>
        [ModelProperty("permissions")]
        public Optional<GuildPermissions> Permissions { get; set; }
        /// <summary> RGB color value. </summary>
        [ModelProperty("color")]
        public Optional<Color> Color { get; set; }
        /// <summary> Whether the <see cref="Role"/> should be displayed seperately in the sidebar. </summary>
        [ModelProperty("hoist")]
        public Optional<bool> IsHoisted { get; set; }
        /// <summary> Whether the <see cref="Role"/> should be mentionable. </summary>
        [ModelProperty("mentionable")]
        public Optional<bool> IsMentionable { get; set; }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Name, nameof(Name));
        }
    }
}
