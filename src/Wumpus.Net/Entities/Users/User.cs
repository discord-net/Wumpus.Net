using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class User
    {
        [ModelProperty("id")]
        public ulong Id { get; set; }
        [ModelProperty("username")]
        public Optional<string> Username { get; set; }
        [ModelProperty("discriminator")]
        public Optional<string> Discriminator { get; set; }
        [ModelProperty("bot")]
        public Optional<bool> Bot { get; set; }
        [ModelProperty("avatar")]
        public Optional<string> Avatar { get; set; }

        //CurrentUser
        [ModelProperty("verified")]
        public Optional<bool> Verified { get; set; }
        [ModelProperty("email")]
        public Optional<string> Email { get; set; }
        [ModelProperty("mfa_enabled")]
        public Optional<bool> MfaEnabled { get; set; }
    }
}
