namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#guild-object-explicit-content-filter-level </summary>
    public enum ExplicitContentFilter
    {
        /// <summary> <see cref="User"/>s will not have their explicit content filtered. </summary>
        Disabled = 0,
        /// <summary> <see cref="User"/>s without roles will have their explicit content filtered. </summary>
        MembersWithoutRoles = 1,
        /// <summary> All <see cref="User"/>s will have their explicit content filtered. </summary>
        AllMembers = 2
    }
}
