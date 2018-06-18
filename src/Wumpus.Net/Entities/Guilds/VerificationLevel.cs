namespace Wumpus.Entities
{
    public enum VerificationLevel
    {
        /// <summary> Users have no additional restrictions on sending messages to this guild. </summary>
        None = 0,
        /// <summary> Users must have a verified email on their account. </summary>
        Low = 1,
        /// <summary> Users must be registered on Discord for longer than 5 minutes. </summary>
        Medium = 2,
        /// <summary> Users must be a member of this guild for longer than 10 minutes. </summary>
        High = 3,
        /// <summary> Users must have a verified phone on their Discord account. </summary>
        VeryHigh = 4
    }
}
