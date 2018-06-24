using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class UserGuild
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public ulong Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public string Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("icon")]
        public string Icon { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("owner")]
        public bool Owner { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("permissions"), Int53]
        public ulong Permissions { get; set; }
    }
}
