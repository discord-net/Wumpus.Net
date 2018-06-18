using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class GetGuildParams
    {
        [ModelProperty("guild_id")]
        public ulong GuildId { get; set; }
    }
}
