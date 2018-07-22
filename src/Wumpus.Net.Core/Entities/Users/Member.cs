using System;
using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class Member
    {
        /// <summary> This <see cref="User"/>'s <see cref="Guild"/> nickname. </summary>
        [ModelProperty("nick")]
        public Optional<Utf8String> Nickname { get; set; }
        /// <summary> Array of <see cref="Role"/> ids. </summary>
        [ModelProperty("roles")]
        public Optional<Snowflake[]> Roles { get; set; }
        /// <summary> When the <see cref="User"/> joined the <see cref="Guild"/>. </summary>
        [ModelProperty("joined_at"), StandardFormat('O')]
        public Optional<DateTimeOffset> JoinedAt { get; set; }
        /// <summary> If the <see cref="User"/> is deafened. </summary>
        [ModelProperty("deaf")]
        public Optional<bool> Deaf { get; set; }
        /// <summary> If the <see cref="User"/> is muted. </summary>
        [ModelProperty("mute")]
        public Optional<bool> Mute { get; set; }
    }
}
