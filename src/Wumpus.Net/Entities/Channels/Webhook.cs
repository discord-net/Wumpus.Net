using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class Webhook
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public ulong Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("guild_id")]
        public Optional<ulong> GuildId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public Optional<ulong> ChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("user")]
        public Optional<User> User { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public string Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("avatar")]
        public Image? Avatar { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("token")]
        public string Token { get; set; }
    }
}
