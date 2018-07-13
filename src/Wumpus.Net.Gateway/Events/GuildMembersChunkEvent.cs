using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> 
    ///     Sent in response to <see cref="GatewayOperation.RequestGuildMembers"/>.
    ///     https://discordapp.com/developers/docs/topics/gateway#guild-members-chunk
    /// </summary>
    public class GuildMembersChunkEvent
    {
        /// <summary> The id of the <see cref="Guild"/>. </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        /// <summary> Set of <see cref="GuildMember"/>s. </summary>
        [ModelProperty("members")]
        public GuildMember[] Members { get; set; }
    }
}
