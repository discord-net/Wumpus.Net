using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class VoiceStateUpdateParams
    {
        [ModelProperty("self_mute")]
        public bool SelfMute { get; set; }
        [ModelProperty("self_deaf")]
        public bool SelfDeaf { get; set; }

        [ModelProperty("guild_id")]
        public ulong? GuildId { get; set; }
        [ModelProperty("channel_id")]
        public ulong? ChannelId { get; set; }
    }
}
