using Voltaic.Serialization;

namespace Wumpus.Responses
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#get-guild-prune-count </summary>
    public class GuildPruneCountResponse
    {
        /// <summary> Number of <see cref="Entities.GuildMember"/>s removed by the prune. </summary>
        [ModelProperty("pruned")]
        public int Pruned { get; set; }
    }
}
