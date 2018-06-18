using Voltaic;
using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Entities
{
    public class Application
    {
        [ModelProperty("description")]
        public string Description { get; set; }
        [ModelProperty("rpc_origins")]
        public string[] RPCOrigins { get; set; }
        [ModelProperty("name")]
        public string Name { get; set; }
        [ModelProperty("id")]
        public ulong Id { get; set; }
        [ModelProperty("icon")]
        public string Icon { get; set; }

        [ModelProperty("flags"), Int53]
        public Optional<ulong> Flags { get; set; }
        [ModelProperty("owner")]
        public Optional<User> Owner { get; set; }
    }
}
