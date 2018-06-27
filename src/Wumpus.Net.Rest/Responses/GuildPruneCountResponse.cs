using Voltaic.Serialization;

namespace Wumpus.Responses
{
    /// <summary> xxx </summary>
    public class GuildPruneCountResponse
    {
        /// <summary> xxx </summary>
        [ModelProperty("pruned")]
        public int Pruned { get; set; }
    }
}
