using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class RpcChannelSummary
    {
        [ModelProperty("id")]
        public ulong Id { get; set; }
        [ModelProperty("name")]
        public string Name { get; set; }
        [ModelProperty("type")]
        public ChannelType Type { get; set; }
    }
}
