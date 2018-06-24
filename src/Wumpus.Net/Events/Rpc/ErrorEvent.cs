using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class ErrorEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("code")]
        public int Code { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("message")]
        public string Message { get; set; }
    }
}
