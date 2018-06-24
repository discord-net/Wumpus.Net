using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class ModifyChannelPermissionsParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("type")]
        public PermissionTarget Type { get; }
        /// <summary> xxx </summary>
        [ModelProperty("allow")]
        public ChannelPermissions Allow { get; }
        /// <summary> xxx </summary>
        [ModelProperty("deny")]
        public ChannelPermissions Deny { get; }

        public ModifyChannelPermissionsParams(PermissionTarget type, ChannelPermissions allow, ChannelPermissions deny)
        {
            Type = type;
            Allow = allow;
            Deny = deny;
        }

        public void Validate() { }
    }
}
