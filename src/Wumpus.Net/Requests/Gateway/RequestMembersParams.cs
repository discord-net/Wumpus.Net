using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/topics/gateway#request-guild-members </summary>
    public class RequestMembersParams
    {
        /// <summary> id of the guild to get offline members for </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        /// <summary> string that username starts with, or an empty string to return all members </summary>
        [ModelProperty("query")]
        public Utf8String Query { get; set; }
        /// <summary> maximum number of members to send or 0 to request all members matched </summary>
        [ModelProperty("limit")]
        public int Limit { get; set; }
    }
}
