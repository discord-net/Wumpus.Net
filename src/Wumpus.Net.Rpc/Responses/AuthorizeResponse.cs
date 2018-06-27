using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class AuthorizeResponse
    {
        /// <summary> xxx </summary>
        [ModelProperty("code")]
        public Utf8String Code { get; set; }
    }
}
