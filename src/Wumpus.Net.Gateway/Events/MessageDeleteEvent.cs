using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> 
    ///     Sent when a <see cref="Entities.Message"/> is deleted.
    ///     https://discordapp.com/developers/docs/topics/gateway#message-delete
    /// </summary>
    public class MessageDeleteEvent
    {
        /// <summary> The id of the <see cref="Entities.Message"/>. </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        // TODO: Undocumented (https://github.com/discordapp/discord-api-docs/issues/582)
        [ModelProperty("guild_id")]
        public Optional<Snowflake> GuildId { get; set; }
        /// <summary> The id of the <see cref="Entities.Channel"/>. </summary>
        [ModelProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
    }
}
