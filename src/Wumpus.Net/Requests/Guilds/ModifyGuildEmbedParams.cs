using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class ModifyGuildEmbedParams
    {        
        [ModelProperty("enabled")]
        public Optional<bool> Enabled { get; set; }
        [ModelProperty("channel")]
        public Optional<ulong?> ChannelId { get; set; }

        public void Validate() { }
    }
}
