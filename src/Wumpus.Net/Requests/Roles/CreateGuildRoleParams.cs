using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    public class CreateGuildRoleParams
    {
        [ModelProperty("name")]
        public Optional<string> Name { get; set; }
        [ModelProperty("permissions")]
        public Optional<GuildPermissions> Permissions { get; set; }
        [ModelProperty("color")]
        public Optional<uint> Color { get; set; }
        [ModelProperty("hoist")]
        public Optional<bool> Hoist { get; set; }
        [ModelProperty("mentionable")]
        public Optional<bool> Mentionable { get; set; }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Name, nameof(Name));
        }
    }
}
