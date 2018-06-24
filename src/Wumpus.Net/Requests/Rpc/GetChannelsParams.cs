using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class GetChannelsParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
    }
}
