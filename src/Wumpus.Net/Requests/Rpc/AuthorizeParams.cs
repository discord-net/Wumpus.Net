using Voltaic.Serialization;
using System.Collections.Generic;

namespace Wumpus.Requests
{
    public class AuthorizeParams
    {
        [ModelProperty("client_id")]
        public string ClientId { get; set; }
        [ModelProperty("scopes")]
        public IReadOnlyCollection<string> Scopes { get; set; }
        [ModelProperty("rpc_token")]
        public Optional<string> RpcToken { get; set; }
    }
}
