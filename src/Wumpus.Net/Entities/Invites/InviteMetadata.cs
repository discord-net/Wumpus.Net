using Voltaic.Serialization;
using System;

namespace Wumpus.Entities
{
    public class InviteMetadata : Invite
    {
        [ModelProperty("inviter")]
        public User Inviter { get; set; }
        [ModelProperty("uses")]
        public int Uses { get; set; }
        [ModelProperty("max_uses")]
        public int MaxUses { get; set; }
        [ModelProperty("max_age")]
        public int MaxAge { get; set; }
        [ModelProperty("temporary")]
        public bool Temporary { get; set; }
        [ModelProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
        [ModelProperty("revoked")]
        public bool Revoked { get; set; }
    }
}
