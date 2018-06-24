using Voltaic;
using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class Role
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("color")]
        public uint Color { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("hoist")]
        public bool Hoist { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("mentionable")]
        public bool Mentionable { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("position")]
        public int Position { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("permissions"), Int53]
        public GuildPermissions Permissions { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("managed")]
        public bool Managed { get; set; }
    }
}
