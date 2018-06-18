using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class GetChannelsParams
    {
        [ModelProperty("guild_id")]
        public ulong GuildId { get; set; }
    }
}
