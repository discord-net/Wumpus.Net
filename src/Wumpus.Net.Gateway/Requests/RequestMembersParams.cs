using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> 
    ///     Used to request offline <see cref="Entities.GuildMember"/> for a <see cref="Entities.Guild"/>. When initially connecting, the gateway will only send offline <see cref="Entities.GuildMember"/>s if a <see cref="Entities.Guild"/> has less than the large_threshold members (value in the Gateway Identify). 
    ///     If a client wishes to receive additional members, they need to explicitly request them via this operation. 
    ///     The server will send <see cref="Events.GatewayDispatchType.GuildMembersChunk"/> events in response with up to 1000 <see cref="Entities.GuildMember"/>s per chunk until all <see cref="Entities.GuildMember"/>s that match the request have been sent.
    ///     https://discordapp.com/developers/docs/topics/gateway#request-guild-members 
    /// </summary>
    public class RequestMembersParams
    {
        /// <summary> Id of the <see cref="Entities.Guild"/> to get offline <see cref="Entities.GuildMember"/>s for. </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        /// <summary> String that username starts with, or an empty string to return all <see cref="Entities.GuildMember"/>s. </summary>
        [ModelProperty("query")]
        public Utf8String Query { get; set; }
        /// <summary> Maximum number of <see cref="Entities.GuildMember"/>s to send or 0 to request all <see cref="Entities.GuildMember"/>s matched. </summary>
        [ModelProperty("limit")]
        public int Limit { get; set; }
    }
}
