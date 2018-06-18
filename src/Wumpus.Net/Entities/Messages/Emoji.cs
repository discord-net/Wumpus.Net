using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class Emoji
    {
        [ModelProperty("id")]
        public ulong? Id { get; set; }
        [ModelProperty("name")]
        public string Name { get; set; }
        [ModelProperty("roles")]
        public ulong[] Roles { get; set; }
        [ModelProperty("require_colons")]
        public bool RequireColons { get; set; }
        [ModelProperty("managed")]
        public bool Managed { get; set; }
    }
}
