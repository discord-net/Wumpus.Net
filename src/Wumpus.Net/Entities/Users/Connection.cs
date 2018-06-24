using Voltaic.Serialization;
using System.Collections.Generic;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class Connection
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public string Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("type")]
        public string Type { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public string Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("revoked")]
        public bool Revoked { get; set; }

        /// <summary> xxx </summary>
        [ModelProperty("integrations")]
        public IReadOnlyCollection<ulong> Integrations { get; set; }
    }
}
