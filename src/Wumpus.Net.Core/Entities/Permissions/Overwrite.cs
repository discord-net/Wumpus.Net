using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#overwrite-object </summary>
    public class Overwrite
    {
        /// <summary> <see cref="Role"/> or <see cref="User"/> id. </summary>
        [ModelProperty("id")]
        public Snowflake TargetId { get; set; }
        /// <summary> Type of the target for this <see cref="Overwrite"/>. </summary>
        [ModelProperty("type")]
        public PermissionTarget TargetType { get; set; }
        /// <summary> Permission bit set. </summary>
        [ModelProperty("allow"), Int53]
        public ChannelPermissions Allow { get; set; }
        /// <summary> Permission bit set. </summary>
        [ModelProperty("deny"), Int53]
        public ChannelPermissions Deny { get; set; }
    }
}
