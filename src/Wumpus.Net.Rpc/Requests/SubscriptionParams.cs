using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class SubscriptionParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public Optional<Snowflake> ChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("guild_id")]
        public Optional<Snowflake> GuildId { get; set; }
    }
}
