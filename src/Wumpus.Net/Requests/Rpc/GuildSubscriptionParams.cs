using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class GuildSubscriptionParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
    }
}
