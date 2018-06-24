using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class RpcChannel
    {
        //Shared
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public ulong Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("type")]
        public ChannelType Type { get; set; }

        //GuildChannel
        /// <summary> xxx </summary>
        [ModelProperty("guild_id")]
        public Optional<ulong> GuildId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Optional<string> Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("position")]
        public Optional<int> Position { get; set; }

        //IMessageChannel
        /// <summary> xxx </summary>
        [ModelProperty("messages")]
        public Message[] Messages { get; set; }

        //VoiceChannel
        /// <summary> xxx </summary>
        [ModelProperty("bitrate")]
        public Optional<int> Bitrate { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("user_limit")]
        public Optional<int> UserLimit { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("voice_states")]
        public RpcVoiceState[] VoiceStates { get; set; }
    }
}
