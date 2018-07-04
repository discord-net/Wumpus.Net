using Voltaic.Serialization;
using System;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/invite#invite-metadata-object </summary>
    public class InviteMetadata : Invite
    {
        /// <summary> <see cref="User"/> who created the <see cref="Invite"/>. </summary>
        [ModelProperty("inviter")]
        public User Inviter { get; set; }
        /// <summary> Number of times this <see cref="Invite"/> has been used. </summary>
        [ModelProperty("uses")]
        public int Uses { get; set; }
        /// <summary> Max number of times this <see cref="Invite"/> can be used. </summary>
        [ModelProperty("max_uses")]
        public int MaxUses { get; set; }
        /// <summary> Duration (in seconds) after which the <see cref="Invite"/> expires. </summary>
        [ModelProperty("max_age")]
        public int MaxAge { get; set; }
        /// <summary> Whether this <see cref="Invite"/> only grants temporary membership. </summary>
        [ModelProperty("temporary")]
        public bool Temporary { get; set; }
        /// <summary> When this <see cref="Invite"/> was created. </summary>
        [ModelProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
        /// <summary> Whether this <see cref="Invite"/> is revoked. </summary>
        [ModelProperty("revoked")]
        public bool Revoked { get; set; }
    }
}
