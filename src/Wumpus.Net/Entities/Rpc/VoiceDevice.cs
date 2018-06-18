using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class VoiceDevice
    {
        [ModelProperty("id")]
        public string Id { get; set; }
        [ModelProperty("name")]
        public string Name { get; set; }
    }
}
