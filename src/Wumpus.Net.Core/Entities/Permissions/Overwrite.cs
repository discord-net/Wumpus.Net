using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class Overwrite
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Snowflake TargetId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("type")]
        public PermissionTarget TargetType { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("allow"), Int53]
        public ChannelPermissions Allow { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("deny"), Int53]
        public ChannelPermissions Deny { get; set; }
    }
}
