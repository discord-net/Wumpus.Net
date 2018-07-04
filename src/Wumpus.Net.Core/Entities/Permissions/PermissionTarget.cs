using Voltaic.Serialization;

namespace Wumpus.Entities
{
    [ModelStringEnum]
    public enum PermissionTarget
    {
        [ModelEnumValue("role")] Role,
        [ModelEnumValue("member")] User
    }
}
