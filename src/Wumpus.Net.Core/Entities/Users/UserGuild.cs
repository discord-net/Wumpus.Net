using Voltaic;
using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/user#get-current-user-guilds-example-partial-guild </summary>
    public class UserGuild
    {
        /// <summary> The <see cref="UserGuild"/>'s id. </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> The <see cref="UserGuild"/>'s name. </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> The <see cref="UserGuild"/>'s icon hash. </summary>
        [ModelProperty("icon")]
        public Utf8String Icon { get; set; }
        /// <summary> If the <see cref="User"/> is the owner of the <see cref="UserGuild"/>. </summary>
        [ModelProperty("owner")]
        public bool Owner { get; set; }
        /// <summary> Permission bit set. </summary>
        [ModelProperty("permissions"), Int53]
        public GuildPermissions Permissions { get; set; }
    }
}
