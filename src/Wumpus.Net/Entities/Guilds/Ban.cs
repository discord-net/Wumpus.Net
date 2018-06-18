using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class Ban
    {
        [ModelProperty("reason")]
        public string Reason { get; set; }
        [ModelProperty("user")]
        public User User { get; set; }
    }
}
