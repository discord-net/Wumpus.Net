using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class SpeakingEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("user_id")]
        public ulong UserId { get; set; }
    }
}
