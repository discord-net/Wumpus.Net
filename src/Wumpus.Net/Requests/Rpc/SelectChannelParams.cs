using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class SelectChannelParams
    {
        [ModelProperty("channel_id")]
        public ulong? ChannelId { get; set; }
        [ModelProperty("force")]
        public Optional<bool> Force { get; set; }
    }
}
