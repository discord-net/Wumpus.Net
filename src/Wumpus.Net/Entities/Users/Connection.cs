using Voltaic.Serialization;
using System.Collections.Generic;
using Voltaic;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class Connection
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Utf8String Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("type")]
        public Utf8String Type { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("revoked")]
        public bool Revoked { get; set; }

        /// <summary> xxx </summary>
        [ModelProperty("integrations")]
        public IReadOnlyCollection<Snowflake> Integrations { get; set; }
    }
}
