using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class SubscriptionResponse
    {
        [ModelProperty("evt")]
        public string Event { get; set; }
    }
}
