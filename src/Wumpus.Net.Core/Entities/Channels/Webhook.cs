using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/webhook </summary>
    public class Webhook
    {
        public const int MinNameLength = 2;
        public const int MaxNameLength = 32;

        /// <summary> The id for the <see cref="Webhook"/>. </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> The <see cref="Guild"/> id this <see cref="Webhook"/> is for. </summary>
        [ModelProperty("guild_id")]
        public Optional<Snowflake> GuildId { get; set; }
        /// <summary> The <see cref="Channel"/> id this <see cref="Webhook"/> is for. </summary>
        [ModelProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        /// <summary> The <see cref="User"/> this <see cref="Webhook"/> was created by. </summary>
        /// <remarks> Not returned when getting a <see cref="Webhook"/> with its token). </remarks>
        [ModelProperty("user")]
        public Optional<User> User { get; set; }
        /// <summary> The default name of the <see cref="Webhook"/>. </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> The default avatar of the <see cref="Webhook"/>. </summary>
        [ModelProperty("avatar")]
        public Image? Avatar { get; set; }
        /// <summary> The secure token of the <see cref="Webhook"/>. </summary>
        [ModelProperty("token")]
        public Utf8String Token { get; set; }
    }
}
