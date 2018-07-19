using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class AuthorizeParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("client_id")]
        public Utf8String ClientId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("scopes")]
        public string[] Scopes { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("rpc_token")]
        public Optional<Utf8String> RpcToken { get; set; }
    }
}
