using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class RpcChannelSummary
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public ulong Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public string Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("type")]
        public ChannelType Type { get; set; }
    }
}
