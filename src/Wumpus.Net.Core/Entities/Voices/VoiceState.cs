using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/voice#voice-state-object </summary>
    public class VoiceState
    {
        /// <summary> The <see cref="Guild"/> id this <see cref="VoiceState"/> is for. </summary>
        [ModelProperty("guild_id")]
        public Optional<Snowflake> GuildId { get; set; }
        /// <summary> The <see cref="Channel"/> id this <see cref="User"/> is connected to. </summary>
        [ModelProperty("channel_id")]
        public Optional<Snowflake?> ChannelId { get; set; }
        /// <summary> The <see cref="User"/> id this <see cref="VoiceState"/> is for. </summary>
        [ModelProperty("user_id")]
        public Snowflake UserId { get; set; }
        // TODO: Undocumented (https://github.com/discordapp/discord-api-docs/issues/582)
        [ModelProperty("member")]
        public Optional<GuildMember> Member { get; set; }
        /// <summary> The session id for this <see cref="VoiceState"/>. </summary>
        [ModelProperty("session_id")]
        public Utf8String SessionId { get; set; }
        /// <summary> Whether this <see cref="User"/> is deafened by the server. </summary>
        [ModelProperty("deaf")]
        public bool Deaf { get; set; }
        /// <summary> Whether this <see cref="User"/> is muted by the server. </summary>
        [ModelProperty("mute")]
        public bool Mute { get; set; }
        /// <summary> Whether this <see cref="User"/> is locally deafened. </summary>
        [ModelProperty("self_deaf")]
        public bool SelfDeaf { get; set; }
        /// <summary> Whether this <see cref="User"/> is locally muted. </summary>
        [ModelProperty("self_mute")]
        public bool SelfMute { get; set; }
        // Undocumented
        [ModelProperty("self_video")]
        public bool SelfVideo { get; set; }
        /// <summary> Whether this <see cref="User"/> is muted by the current <see cref="User"/>. </summary>
        [ModelProperty("suppress")]
        public bool Suppress { get; set; }
    }
}
