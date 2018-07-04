namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#guild-object-verification-level </summary>
    public enum VerificationLevel
    {
        /// <summary> <see cref="GuildMember"/>s have no additional restrictions on sending <see cref="Message"/>s to this <see cref="Guild"/>. </summary>
        None = 0,
        /// <summary> <see cref="GuildMember"/>s must have a verified email on their account. </summary>
        Low = 1,
        /// <summary> <see cref="GuildMember"/>s must be registered on Discord for longer than 5 minutes. </summary>
        Medium = 2,
        /// <summary> <see cref="GuildMember"/>s must be a member of this <see cref="Guild"/> for longer than 10 minutes. </summary>
        High = 3,
        /// <summary> <see cref="GuildMember"/>s must have a verified phone on their Discord account. </summary>
        VeryHigh = 4
    }
}
