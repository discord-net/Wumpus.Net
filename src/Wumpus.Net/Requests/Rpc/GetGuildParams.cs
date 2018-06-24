using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class GetGuildParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("guild_id")]
        public ulong GuildId { get; set; }
    }
}
