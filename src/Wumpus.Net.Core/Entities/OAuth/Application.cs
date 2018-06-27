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
        public Utf8String Description { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("rpc_origins")]
        public Utf8String[] RPCOrigins { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("icon")]
        public Utf8String Icon { get; set; }

        /// <summary> xxx </summary>
        [ModelProperty("flags"), Int53]
        public Optional<ulong> Flags { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("owner")]
        public Optional<User> Owner { get; set; }
    }
}
