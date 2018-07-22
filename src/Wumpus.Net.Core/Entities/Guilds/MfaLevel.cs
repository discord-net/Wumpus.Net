namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#guild-object-mfa-level </summary>
    public enum MfaLevel
    {
        /// <summary> <see cref="User" />s have no additional MFA restriction on this guild. </summary>
        None = 0,
        /// <summary> <see cref="User" />s must have MFA enabled on their account to perform administrative actions. </summary>
        Elevated = 1
    }
}
