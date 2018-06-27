using Voltaic.Serialization;
using System;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class InviteMetadata : Invite
    {
        /// <summary> xxx </summary>
        [ModelProperty("inviter")]
        public User Inviter { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("uses")]
        public int Uses { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("max_uses")]
        public int MaxUses { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("max_age")]
        public int MaxAge { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("temporary")]
        public bool Temporary { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("revoked")]
        public bool Revoked { get; set; }
    }
}
