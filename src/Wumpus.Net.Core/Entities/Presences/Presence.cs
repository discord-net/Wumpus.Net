using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/topics/gateway#presence-update-presence-update-event-fields </summary>
    [IgnoreErrors]
    [IgnorePropertiesAttribute("nick")]
    public class Presence
    {
        /// <summary> The <see cref="User"/> presence is being updated for. </summary>
        [ModelProperty("user")]
        public User User { get; set; }
        /// <summary> Id of the <see cref="Guild"/>. </summary>
        [ModelProperty("guild_id")]
        public Optional<Snowflake?> GuildId { get; set; }
        /// <summary> Either "idle", "dnd", "online", or "offline". </summary>
        [ModelProperty("status")]
        public Optional<UserStatus> Status { get; set; }
        /// <summary> Null, or the <see cref="Entities.User"/>'s current <see cref="Activity"/>. </summary>
        [ModelProperty("game")]
        public Optional<Activity> Game { get; set; }
        /// <summary> Roles this <see cref="Entities.User"/> is in. </summary>
        [ModelProperty("roles")]
        public Optional<Snowflake[]> Roles { get; set; }
    }
}
