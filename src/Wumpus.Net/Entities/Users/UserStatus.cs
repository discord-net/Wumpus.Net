using Voltaic.Serialization;

namespace Wumpus.Entities
{
    [ModelStringEnum]
    public enum UserStatus
    {
        [ModelEnumValue("offline", EnumValueType.ReadOnly)]
        Offline,
        [ModelEnumValue("online")]
        Online,
        [ModelEnumValue("idle")]
        Idle,
        [ModelEnumValue("idle", EnumValueType.WriteOnly)]
        AFK,
        [ModelEnumValue("dnd")]
        DoNotDisturb,
        [ModelEnumValue("invisible", EnumValueType.WriteOnly)]
        Invisible,
    }
}
