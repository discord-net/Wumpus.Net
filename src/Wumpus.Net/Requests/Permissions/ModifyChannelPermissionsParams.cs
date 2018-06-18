using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class ModifyChannelPermissionsParams
    {
        [ModelProperty("type")]
        public PermissionTarget Type { get; }
        [ModelProperty("allow")]
        public ChannelPermissions Allow { get; }
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
