
using Voltaic.Serialization;
using Wumpus.Entities;

namespace Wumpus.Events
{
    /// <summary>
    ///     Sent when a user removes a reaction from a message.
    ///     https://discordapp.com/developers/docs/topics/gateway#message-reaction-remove
    /// </summary>
    public class MessageReactionRemoveEvent
    {
        /// <summary> The id of the <see cref="User"/>. </summary>
        [ModelProperty("user_id")]
        public Snowflake UserId { get; set; }
        // TODO: Undocumented (https://github.com/discordapp/discord-api-docs/issues/582)
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        /// <summary> The id of the <see cref="Channel"/>. </summary>
        [ModelProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        /// <summary> The id of the <see cref="Message"/>. </summary>
        [ModelProperty("message_id")]
        public Snowflake MessageId { get; set; }
        /// <summary> A partial <see cref="Emoji"/> object used to react. </summary>
        [ModelProperty("emoji")]
        public Emoji Emoji { get; set; }

    }
}
