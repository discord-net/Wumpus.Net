using Voltaic;
using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/topics/permissions#role-object </summary>
    public class Role
    {
        /// <summary> <see cref="Role"/> id. </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> <see cref="Role"/> name. </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> Integer representation of hexadecimal color code. </summary>
        [ModelProperty("color")]
        public Color Color { get; set; }
        /// <summary> If this <see cref="Role"/> is pinned in the <see cref="User"/> listing. </summary>
        [ModelProperty("hoist")]
        public bool IsHoisted { get; set; }
        /// <summary> Whether this <see cref="Role"/> is mentionable. </summary>
        [ModelProperty("mentionable")]
        public bool IsMentionable { get; set; }
        /// <summary> Position of this <see cref="Role"/>. </summary>
        [ModelProperty("position")]
        public int Position { get; set; }
        /// <summary> Permission bit set. </summary>
        [ModelProperty("permissions"), Int53]
        public GuildPermissions Permissions { get; set; }
        /// <summary> Whether this <see cref="Role"/> is managed by an <see cref="Integration"/>. </summary>
        [ModelProperty("managed")]
        public bool Managed { get; set; }
    }
}
