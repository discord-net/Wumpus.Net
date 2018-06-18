using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class GetChannelParams
    {
        [ModelProperty("channel_id")]
        public ulong ChannelId { get; set; }
    }
}
