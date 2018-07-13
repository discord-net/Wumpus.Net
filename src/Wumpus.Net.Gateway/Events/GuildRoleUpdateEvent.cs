using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> 
    ///     Sent when a <see cref="Guild"/> <see cref="Entities.Role"/> is updated.
    ///     https://discordapp.com/developers/docs/topics/gateway#guild-role-update
    /// </summary>
    public class GuildRoleUpdateEvent
    {
        /// <summary> The id of the <see cref="Guild"/>. </summary>
		[ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        /// <summary> The <see cref="Entities.Role"/> updated. </summary>
        [ModelProperty("role")]
        public Role Role { get; set; }
    }
}
