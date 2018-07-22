using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Events
{
    /// <summary> 
    ///     Sent when a <see cref="GuildMember"/> is updated.
    ///     https://discordapp.com/developers/docs/topics/gateway#guild-member-update
    /// </summary>
    public class GuildMemberUpdateEvent : GuildMember
    {
        /// <summary> The id of the <see cref="Guild"/>. </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
    }
}
