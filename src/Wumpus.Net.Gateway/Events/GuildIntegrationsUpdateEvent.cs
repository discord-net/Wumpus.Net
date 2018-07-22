using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> 
    ///     Sent when a guild integration is updated.
    ///     https://discordapp.com/developers/docs/topics/gateway#guild-integrations-update
    /// </summary>
    public class GuildIntegrationsUpdateEvent
    {
        /// <summary> Id of the <see cref="Guild"/> whose integrations were updated. </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
    }
}
