using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class RpcGuildSummary
    {
        [ModelProperty("id")]
        public ulong Id { get; set; }
        [ModelProperty("name")]
        public string Name { get; set; }
    }
}
