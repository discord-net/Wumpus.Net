using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#edit-channel-permissions-json-params </summary>
    public class ModifyChannelPermissionsParams
    {
        /// <summary> "Member" for a <see cref="User"/> or "Role" for a <see cref="Role"/>. </summary>
        [ModelProperty("type")]
        public PermissionTarget Type { get; private set; }
        /// <summary> The bitwise value for all allowed permissions. </summary>
        [ModelProperty("allow")]
        public ChannelPermissions Allow { get; private set; }
        /// <summary> The bitwise value of all disallowed permissions. </summary>
        [ModelProperty("deny")]
        public ChannelPermissions Deny { get; private set; }

        public ModifyChannelPermissionsParams(PermissionTarget type, ChannelPermissions allow, ChannelPermissions deny)
        {
            Type = type;
            Allow = allow;
            Deny = deny;
        }

        public void Validate() { }
    }
}
