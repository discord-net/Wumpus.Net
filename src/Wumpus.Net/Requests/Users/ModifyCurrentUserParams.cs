using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class ModifyCurrentUserParams
    {
        [ModelProperty("username")]
        public Optional<string> Username { get; set; }
        [ModelProperty("avatar")]
        public Optional<Image?> Avatar { get; set; }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Username, nameof(Username));
        }
    }
}
