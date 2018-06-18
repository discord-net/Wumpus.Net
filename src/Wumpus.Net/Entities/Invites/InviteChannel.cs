using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class InviteChannel
    {
        [ModelProperty("id")]
        public ulong Id { get; set; }
        [ModelProperty("name")]
        public string Name { get; set; }
        [ModelProperty("type")]
        public string Type { get; set; }
    }
}
