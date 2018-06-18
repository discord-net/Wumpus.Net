namespace Wumpus.Entities
{
    public enum MfaLevel
    {
        /// <summary> Users have no additional MFA restriction on this guild. </summary>
        None = 0,
        /// <summary> Users must have MFA enabled on their account to perform administrative actions. </summary>
        Elevated = 1
    }
}
