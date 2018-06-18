using Voltaic.Serialization;
using System.Collections.Generic;

namespace Wumpus.Entities
{
    public class RpcGuild
    {
        [ModelProperty("id")]
        public ulong Id { get; set; }
        [ModelProperty("name")]
        public string Name { get; set; }
        [ModelProperty("icon_url")]
        public string IconUrl { get; set; }
        [ModelProperty("members")]
        public IEnumerable<GuildMember> Members { get; set; }
    }
}
