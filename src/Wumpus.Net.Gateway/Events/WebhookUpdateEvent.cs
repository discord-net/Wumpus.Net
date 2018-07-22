using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary>
    ///     Sent when a <see cref="Entities.Guild"/> <see cref="Entities.Channel"/>'s <see cref="Entities.Webhook"/> is created, updated, or deleted.
    ///     https://discordapp.com/developers/docs/topics/gateway#webhooks-update
    /// </summary>
    public class WebhooksUpdateEvent
    {
        /// <summary> Id of the <see cref="Entities.Guild"/>. </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        /// <summary> Id of the <see cref="Entities.Channel"/>. </summary>
        [ModelProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
    }
}
