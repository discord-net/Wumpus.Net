using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class Webhook
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("guild_id")]
        public Optional<Snowflake> GuildId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public Optional<Snowflake> ChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("user")]
        public Optional<User> User { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("avatar")]
        public Image? Avatar { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("token")]
        public Utf8String Token { get; set; }
    }
}
