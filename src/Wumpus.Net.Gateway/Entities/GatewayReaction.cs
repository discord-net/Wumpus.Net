using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> 
    ///     https://discordapp.com/developers/docs/topics/gateway#message-reaction-add 
    ///     https://discordapp.com/developers/docs/topics/gateway#message-reaction-remove 
    /// </summary>
    public class GatewayReaction
    {
        /// <summary> The id of the <see cref="User"/>. </summary>
        [ModelProperty("user_id")]
        public Snowflake UserId { get; set; }
        /// <summary> The id of the <see cref="Message"/>. </summary>
        [ModelProperty("message_id")]
        public Snowflake MessageId { get; set; }
        /// <summary> The id of the <see cref="Channel"/>. </summary>
        [ModelProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        /// <summary> The <see cref="Entities.Emoji"/> used to react. </summary>
        [ModelProperty("emoji")]
        public Emoji Emoji { get; set; }
    }
}
