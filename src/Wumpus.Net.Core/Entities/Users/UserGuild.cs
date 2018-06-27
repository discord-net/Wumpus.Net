using Voltaic;
using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class UserGuild
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("icon")]
        public Utf8String Icon { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("owner")]
        public bool Owner { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("permissions"), Int53]
        public GuildPermissions Permissions { get; set; }
    }
}
