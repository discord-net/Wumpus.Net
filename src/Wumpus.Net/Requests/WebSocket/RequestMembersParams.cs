using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class RequestMembersParams
    {
        [ModelProperty("query")]
        public string Query { get; set; }
        [ModelProperty("limit")]
        public int Limit { get; set; }

        [ModelProperty("guild_id")]
        public ulong[] GuildIds { get; set; }
    }
}
