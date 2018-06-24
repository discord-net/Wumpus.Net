using Voltaic.Serialization;
using System.Collections.Generic;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class AuthorizeParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("client_id")]
        public string ClientId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("scopes")]
        public IReadOnlyCollection<string> Scopes { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("rpc_token")]
        public Optional<string> RpcToken { get; set; }
    }
}
