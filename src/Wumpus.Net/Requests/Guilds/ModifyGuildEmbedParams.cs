using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class ModifyGuildEmbedParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("enabled")]
        public Optional<bool> Enabled { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("channel")]
        public Optional<ulong?> ChannelId { get; set; }

        public void Validate() { }
    }
}
