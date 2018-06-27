using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    [ModelStringEnum]
    public enum PermissionTarget
    {
        [ModelEnumValue("role")] Role,
        [ModelEnumValue("member")] User
    }
}
