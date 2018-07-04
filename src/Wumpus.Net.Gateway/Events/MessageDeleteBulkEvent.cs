using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> 
    ///     Sent when multiple messages are deleted at once.
    ///     https://discordapp.com/developers/docs/topics/gateway#message-delete-bulk
    /// </summary>
    public class MessageDeleteBulkEvent
    {
        /// <summary> The id of the <see cref="Entities.Channel"/>. </summary>
        [ModelProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        /// <summary> The ids of the <see cref="Entities.Message"/>s. </summary>
        [ModelProperty("ids")]
        public Snowflake[] Ids { get; set; }
    }
}
