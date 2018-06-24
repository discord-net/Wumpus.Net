using Voltaic;
using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class Application
    {
        /// <summary> xxx </summary>
        [ModelProperty("description")]
        public string Description { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("rpc_origins")]
        public string[] RPCOrigins { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public string Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public ulong Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("icon")]
        public string Icon { get; set; }

        /// <summary> xxx </summary>
        [ModelProperty("flags"), Int53]
        public Optional<ulong> Flags { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("owner")]
        public Optional<User> Owner { get; set; }
    }
}
