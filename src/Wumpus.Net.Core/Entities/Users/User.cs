using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/user#user-object </summary>
    public class User
    {
        /// <summary> The <see cref="User"/>'s id. </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> The <see cref="User"/>'s username, not unique across the platform. </summary>
        [ModelProperty("username")]
        public Optional<Utf8String> Username { get; set; }
        /// <summary> The <see cref="User"/>'s 4-digit discord-tag. </summary>
        [ModelProperty("discriminator")]
        public Optional<Utf8String> Discriminator { get; set; }
        /// <summary> Whether the <see cref="User"/> belongs to an OAuth2 application. </summary>
        [ModelProperty("bot")]
        public Optional<bool> Bot { get; set; }
        /// <summary> The <see cref="User"/>'s avatar hash. </summary>
        [ModelProperty("avatar")]
        public Optional<Image?> Avatar { get; set; }

        //CurrentUser
        /// <summary> Whether the email on this account has been verified. </summary>
        [ModelProperty("verified")]
        public Optional<bool> Verified { get; set; }
        /// <summary> The <see cref="User"/>'s email. </summary>
        [ModelProperty("email")]
        public Optional<Utf8String> Email { get; set; }
        /// <summary> Whether the user has two factor enabled on their account. </summary>
        [ModelProperty("mfa_enabled")]
        public Optional<bool> MfaEnabled { get; set; }
    }
}
