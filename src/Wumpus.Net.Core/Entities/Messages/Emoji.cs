using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/emoji#emoji-object </summary>
    public class Emoji
    {
        /// <summary> <see cref="Emoji"/> id. </summary>
        [ModelProperty("id")]
        public Snowflake? Id { get; set; }
        /// <summary> <see cref="Emoji"/> name. </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> <see cref="Role"/>s this <see cref="Emoji"/> is whitelisted to. </summary>
        [ModelProperty("roles")]
        public Optional<Snowflake[]> RoleIds { get; set; }
        /// <summary> <see cref="Entities.User"/> that created this <see cref="Emoji"/>. </summary>
        [ModelProperty("user")]
        public Optional<EntityOrId<User>> User { get; set; }  
        /// <summary> Whether this <see cref="Emoji"/> must be wrapped in colons. </summary>
        [ModelProperty("require_colons")]
        public Optional<bool> RequireColons { get; set; }
        /// <summary> Whether this <see cref="Emoji"/> is managed. </summary>
        [ModelProperty("managed")]
        public Optional<bool> Managed { get; set; }
        /// <summary> Whether this <see cref="Emoji"/> is animated. </summary>
        [ModelProperty("animated")]
        public Optional<bool> Animated { get; set; }
    }
}
