using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class RpcChannel
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("guild_id")]
        public Optional<Snowflake> GuildId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Optional<Utf8String> Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("type")]
        public ChannelType Type { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("bitrate")]
        public Optional<int> Bitrate { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("user_limit")]
        public Optional<int> UserLimit { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("position")]
        public Optional<int> Position { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("voice_states")]
        public VoiceState[] VoiceStates { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("messages")]
        public Message[] Messages { get; set; }
    }
}
