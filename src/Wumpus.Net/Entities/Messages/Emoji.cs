using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class Emoji
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Snowflake? Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("roles")]
        public Snowflake[] Roles { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("require_colons")]
        public bool RequireColons { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("managed")]
        public bool Managed { get; set; }
    }
}
