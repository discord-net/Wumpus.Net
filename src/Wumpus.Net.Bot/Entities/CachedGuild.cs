using Wumpus.Entities;

namespace Wumpus.Bot
{
    public class CachedGuild : GatewayGuild
    {
        internal void Update(GatewayGuild data)
        {
            // Unavailable = data.Unavailable; // This is handled manually in GuildCache
            Update(data as Guild);
        }
        internal void Update(Guild data)
        {
        }
    }
}
