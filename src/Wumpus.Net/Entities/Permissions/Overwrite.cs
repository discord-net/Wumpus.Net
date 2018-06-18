using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Entities
{
    public class Overwrite
    {
        [ModelProperty("id")]
        public ulong TargetId { get; set; }
        [ModelProperty("type")]
        public PermissionTarget TargetType { get; set; }
        [ModelProperty("allow"), Int53]
        public ChannelPermissions Allow { get; set; }
        [ModelProperty("deny"), Int53]
        public ChannelPermissions Deny { get; set; }
    }
}
