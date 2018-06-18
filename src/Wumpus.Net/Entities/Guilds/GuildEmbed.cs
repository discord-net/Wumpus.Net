using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class GuildEmbed
    {
        [ModelProperty("enabled")]
        public bool Enabled { get; set; }
        [ModelProperty("channel_id")]
        public ulong ChannelId { get; set; }
    }
}
