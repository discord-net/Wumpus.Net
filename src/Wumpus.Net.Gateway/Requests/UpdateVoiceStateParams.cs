using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> 
    ///     Sent when a client wants to join, move, or disconnect from a voice <see cref="Entities.Channel"/>.
    ///     https://discordapp.com/developers/docs/topics/gateway#update-voice-state 
    /// </summary>
    public class UpdateVoiceStateParams
    {
        /// <summary> Id of the <see cref="Entities.Guild"/>. </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        /// <summary> Id of the voice <see cref="Entities.Channel"/> client wants to join (null if disconnecting). </summary>
        [ModelProperty("channel_id")]
        public Snowflake? ChannelId { get; set; }
        /// <summary> Is the client muted? </summary>
        [ModelProperty("self_mute")]
        public bool SelfMute { get; set; }
        /// <summary> Is the client deafened? </summary>
        [ModelProperty("self_deaf")]
        public bool SelfDeaf { get; set; }
    }
}
