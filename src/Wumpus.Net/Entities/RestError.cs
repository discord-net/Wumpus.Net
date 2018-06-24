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
        public string Message { get; set; }
    }
}
