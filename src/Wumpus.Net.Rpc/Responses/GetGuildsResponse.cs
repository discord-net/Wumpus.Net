using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class GetGuildsResponse
    {
        /// <summary> xxx </summary>
        [ModelProperty("guilds")]
        public RpcGuild[] Guilds { get; set; }
    }
}
