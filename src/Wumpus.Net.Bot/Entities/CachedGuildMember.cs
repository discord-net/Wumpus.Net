using Wumpus.Entities;

namespace Wumpus.Bot
{
    public class CachedGuildMember : Member
    {
        public Snowflake GuildId { get; set; }
        public Snowflake UserId { get; set; }
    }
}
