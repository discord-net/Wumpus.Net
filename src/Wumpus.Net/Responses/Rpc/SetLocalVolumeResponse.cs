using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class SetLocalVolumeResponse
    {
        [ModelProperty("user_id")]
        public ulong UserId { get; set; }
        [ModelProperty("volume")]
        public int Volume { get; set; }
    }
}
