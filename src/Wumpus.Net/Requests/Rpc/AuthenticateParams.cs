using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class AuthenticateParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
