using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class AuthenticateParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("access_token")]
        public Utf8String AccessToken { get; set; }
    }
}
