using Voltaic.Serialization;
using System;
using Voltaic;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class GuildMember
    {
        /// <summary> xxx </summary>
        [ModelProperty("user")]
        public User User { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("nick")]
        public Optional<string> Nick { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("roles")]
        public Optional<ulong[]> Roles { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("joined_at")]
        public Optional<DateTimeOffset> JoinedAt { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("deaf")]
        public Optional<bool> Deaf { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("mute")]
        public Optional<bool> Mute { get; set; }
    }
}
