using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> 
    ///     Sent when multiple <see cref="Entities.Message"/>s are deleted at once.
    ///     https://discordapp.com/developers/docs/topics/gateway#message-delete-bulk
    /// </summary>
    public class MessageDeleteBulkEvent
    {
        /// <summary> The ids of the <see cref="Entities.Message"/>s. </summary>
        [ModelProperty("ids")]
        public Snowflake[] Ids { get; set; }
        // TODO: Undocumented (https://github.com/discordapp/discord-api-docs/issues/582)
        [ModelProperty("guild_id")]
        public Optional<Snowflake?> GuildId { get; set; }
        /// <summary> The id of the <see cref="Entities.Channel"/>. </summary>
        [ModelProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
    }
}
