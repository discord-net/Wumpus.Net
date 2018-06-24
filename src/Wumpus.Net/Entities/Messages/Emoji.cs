using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class Emoji
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public ulong? Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public string Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("roles")]
        public ulong[] Roles { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("require_colons")]
        public bool RequireColons { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("managed")]
        public bool Managed { get; set; }
    }
}
