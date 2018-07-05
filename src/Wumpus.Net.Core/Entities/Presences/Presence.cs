using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    [IgnoreErrors]
    public class Presence
    {
        /// <summary> xxx </summary>
        [ModelProperty("user")]
        public User User { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("guild_id")]
        public Optional<Snowflake> GuildId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("status")]
        public UserStatus Status { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("game")]
        public Activity Game { get; set; }

        /// <summary> xxx </summary>
        [ModelProperty("roles")]
        public Optional<Snowflake[]> Roles { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("nick")]
        public Optional<Utf8String> Nick { get; set; }
    }
}
