using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/topics/gateway#update-status-status-types </summary>
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
