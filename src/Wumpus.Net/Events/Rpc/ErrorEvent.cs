using Voltaic;
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
        public Utf8String Message { get; set; }
    }
}
