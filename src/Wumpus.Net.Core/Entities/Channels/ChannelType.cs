namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#channel-object-channel-types </summary>
    public enum ChannelType : byte
    {
        Text = 0,
        DM = 1,
        Voice = 2,
        Group = 3,
        Category = 4
    }
}
