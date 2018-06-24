using Voltaic.Serialization;
using System.Collections.Generic;
using Voltaic;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class RpcGuild
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("icon_url")]
        public Utf8String IconUrl { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("members")]
        public IEnumerable<GuildMember> Members { get; set; }
    }
}
