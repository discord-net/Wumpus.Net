using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Entities
{
    public class UserGuild
    {
        [ModelProperty("id")]
        public ulong Id { get; set; }
        [ModelProperty("name")]
        public string Name { get; set; }
        [ModelProperty("icon")]
        public string Icon { get; set; }
        [ModelProperty("owner")]
        public bool Owner { get; set; }
        [ModelProperty("permissions"), Int53]
        public ulong Permissions { get; set; }
    }
}
