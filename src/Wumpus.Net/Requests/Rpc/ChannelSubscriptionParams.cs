using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class ChannelSubscriptionParams
    {
        [ModelProperty("channel_id")]
        public ulong ChannelId { get; set; }
    }
}
