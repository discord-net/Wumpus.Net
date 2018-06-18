using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class GuildSubscriptionParams
    {
        [ModelProperty("guild_id")]
        public ulong GuildId { get; set; }
    }
}
