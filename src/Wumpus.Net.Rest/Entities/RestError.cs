using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class RestError
    {
        /// <summary> xxx </summary>
        [ModelProperty("code")]
        public int? Code { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("message")]
        public Utf8String Message { get; set; }
    }
}
