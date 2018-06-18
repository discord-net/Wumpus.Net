using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class GetGuildsResponse
    {
        [ModelProperty("guilds")]
        public RpcGuildSummary[] Guilds { get; set; }
    }
}
