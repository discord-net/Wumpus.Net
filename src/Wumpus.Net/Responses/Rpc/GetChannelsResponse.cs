using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class GetChannelsResponse
    {
        [ModelProperty("channels")]
        public RpcChannelSummary[] Channels { get; set; }
    }
}
