using Voltaic.Serialization;

namespace Wumpus.Responses
{
    public class GuildPruneCountResponse
    {
        [ModelProperty("pruned")]
        public int Pruned { get; set; }
    }
}
