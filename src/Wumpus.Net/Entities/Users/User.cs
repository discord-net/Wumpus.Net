using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class User
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public ulong Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("username")]
        public Optional<string> Username { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("discriminator")]
        public Optional<string> Discriminator { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("bot")]
        public Optional<bool> Bot { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("avatar")]
        public Optional<string> Avatar { get; set; }

        //CurrentUser
        /// <summary> xxx </summary>
        [ModelProperty("verified")]
        public Optional<bool> Verified { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("email")]
        public Optional<string> Email { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("mfa_enabled")]
        public Optional<bool> MfaEnabled { get; set; }
    }
}
