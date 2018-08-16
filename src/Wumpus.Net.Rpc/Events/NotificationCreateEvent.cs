using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class NotificationCreateEvent
    {
        [ModelProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        [ModelProperty("message")]
        public Message Message { get; set; }
        [ModelProperty("icon_url")]
        public Utf8String IconUrl { get; set; }
        [ModelProperty("title")]
        public Utf8String Title { get; set; }
        [ModelProperty("body")]
        public Utf8String Body { get; set; }
    }
}