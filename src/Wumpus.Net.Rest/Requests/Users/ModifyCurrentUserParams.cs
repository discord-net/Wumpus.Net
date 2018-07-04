using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/user#modify-current-user-json-params </summary>
    public class ModifyCurrentUserParams
    {
        /// <summary> <see cref="Entities.User"/>'s username, if changed may cause the <see cref="Entities.User"/>'s discriminator to be randomized. </summary>
        [ModelProperty("username")]
        public Optional<Utf8String> Username { get; set; }
        /// <summary> If passed, modifies the <see cref="Entities.User"/>'s avatar. </summary>
        [ModelProperty("avatar")]
        public Optional<Image?> Avatar { get; set; }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Username, nameof(Username));
        }
    }
}
