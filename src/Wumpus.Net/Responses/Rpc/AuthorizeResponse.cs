using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class AuthorizeResponse
    {
        /// <summary> xxx </summary>
        [ModelProperty("code")]
        public string Code { get; set; }
    }
}
