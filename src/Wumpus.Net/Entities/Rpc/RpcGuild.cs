using Voltaic.Serialization;
using System.Collections.Generic;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class RpcGuild
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public ulong Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public string Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("icon_url")]
        public string IconUrl { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("members")]
        public IEnumerable<GuildMember> Members { get; set; }
    }
}
