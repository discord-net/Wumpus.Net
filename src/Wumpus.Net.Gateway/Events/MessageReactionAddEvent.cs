
using Voltaic.Serialization;
using Wumpus.Entities;

namespace Wumpus.Events
{
    /// <summary>
    ///     Sent when a user adds a reaction to a message.
    ///     https://discordapp.com/developers/docs/topics/gateway#message-reaction-add
    /// </summary>
    public class MessageReactionAddEvent
    {
        /// <summary> The id of the <see cref="User"/>. </summary>
        [ModelProperty("user_id")]
        public Snowflake UserId { get; set; }
        /// <summary> The id of the <see cref="Channel"/>. </summary>
        [ModelProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        /// <summary> The id of the <see cref="Message"/>. </summary>
        [ModelProperty("message_Id")]
        public Snowflake MessageId { get; set; }
        /// <summary> A partial <see cref="Emoji"/> object used to react. </summary>
        [ModelProperty("emoji")]
        public Emoji Emoji { get; set; }

    }
}
