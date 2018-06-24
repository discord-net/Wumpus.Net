using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/topics/gateway#update-voice-state </summary>
    public class UpdateVoiceStateParams
    {
        /// <summary> id of the guild </summary>
        [ModelProperty("guild_id")]
        public ulong GuildId { get; set; }
        /// <summary> id of the voice channel client wants to join (null if disconnecting) </summary>
        [ModelProperty("channel_id")]
        public ulong? ChannelId { get; set; }
        /// <summary> is the client muted </summary>
        [ModelProperty("self_mute")]
        public bool SelfMute { get; set; }
        /// <summary> is the client deafened </summary>
        [ModelProperty("self_deaf")]
        public bool SelfDeaf { get; set; }
    }
}
