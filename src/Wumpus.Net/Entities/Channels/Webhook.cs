using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class Webhook
    {
        [ModelProperty("id")]
        public ulong Id { get; set; }
        [ModelProperty("guild_id")]
        public Optional<ulong> GuildId { get; set; }
        [ModelProperty("channel_id")]
        public Optional<ulong> ChannelId { get; set; }
        [ModelProperty("user")]
        public Optional<User> User { get; set; }
        [ModelProperty("name")]
        public string Name { get; set; }
        [ModelProperty("avatar")]
        public Image? Avatar { get; set; }
        [ModelProperty("token")]
        public string Token { get; set; }
    }
}
