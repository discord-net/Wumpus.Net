using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class ModifyCurrentUserParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("username")]
        public Optional<Utf8String> Username { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("avatar")]
        public Optional<Image?> Avatar { get; set; }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Username, nameof(Username));
        }
    }
}
