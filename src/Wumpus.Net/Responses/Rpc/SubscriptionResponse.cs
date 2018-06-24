using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class SubscriptionResponse
    {
        /// <summary> xxx </summary>
        [ModelProperty("evt")]
        public string Event { get; set; }
    }
}
