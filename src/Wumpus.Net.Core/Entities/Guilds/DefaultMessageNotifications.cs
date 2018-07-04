namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#guild-object-default-message-notification-level </summary>
    public enum DefaultMessageNotifications
    {
        /// <summary> By default, all <see cref="Message"/>s will trigger notifications. </summary>
        AllMessages = 0,
        /// <summary> By default, only mentions will trigger notifications. </summary>
        MentionsOnly = 1
    }
}
