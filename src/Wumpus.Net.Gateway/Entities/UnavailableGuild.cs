using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class UnavailableGuild
    {
        /// <summary> <see cref="Guild"/> id. </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> Is this <see cref="Guild"/> unavailable? </summary>
        [ModelProperty("unavailable")]
        public Optional<bool> Unavailable { get; set; }
    }
}
