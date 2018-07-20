using Voltaic.Serialization;
using System;
using Voltaic;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#guild-embed-object </summary>
    public class GuildMember
    {
        /// <summary> <see cref="User"/> object. </summary>
        [ModelProperty("user")]
        public User User { get; set; }
        /// <summary> This user's <see cref="Guild"/> nickname. </summary>
        [ModelProperty("nick")]
        public Optional<Utf8String> Nickname { get; set; }
        /// <summary> Array of <see cref="Role"/> ids. </summary>
        [ModelProperty("roles")]
        public Optional<Snowflake[]> Roles { get; set; }
        /// <summary> When the <see cref="GuildMember"/> joined the <see cref="Guild"/>. </summary>
        [ModelProperty("joined_at"), StandardFormat('O')]
        public Optional<DateTimeOffset> JoinedAt { get; set; }
        /// <summary> If the <see cref="GuildMember"/> is deafened. </summary>
        [ModelProperty("deaf")]
        public Optional<bool> Deaf { get; set; }
        /// <summary> If the <see cref="GuildMember"/> is muted. </summary>
        [ModelProperty("mute")]
        public Optional<bool> Mute { get; set; }
    }
}
