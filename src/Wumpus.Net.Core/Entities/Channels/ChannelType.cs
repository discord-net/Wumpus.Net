namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#channel-object-channel-types </summary>
    public enum ChannelType : byte
    {
        Text = 0,
        Dm = 1,
        Voice = 2,
        GroupDm = 3,
        Category = 4
    }
}

