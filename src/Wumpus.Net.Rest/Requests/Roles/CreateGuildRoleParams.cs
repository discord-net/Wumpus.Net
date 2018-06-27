using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class CreateGuildRoleParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Optional<Utf8String> Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("permissions")]
        public Optional<GuildPermissions> Permissions { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("color")]
        public Optional<uint> Color { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("hoist")]
        public Optional<bool> Hoist { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("mentionable")]
        public Optional<bool> Mentionable { get; set; }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Name, nameof(Name));
        }
    }
}
