using Voltaic.Serialization;
using System;
using Voltaic;
using System.Collections.Generic;
using Wumpus.Requests;

namespace Wumpus
{
    public class RpcPayload
    {
        /// <summary> Payload command </summary>
        [ModelProperty("cmd")]
        public RpcCommand Cmd { get; set; }
        /// <summary> Subscription event </summary>
        [ModelProperty("evt")]
        public Optional<RpcEvent> Event { get; set; } 
        /// <summary> Unique string used once for replies from the server	 </summary>
        [ModelProperty("nonce")]
        public Optional<Guid?> Nonce { get; set; }        

        /// <summary> xxx </summary>
        [ModelProperty("args"), ModelTypeSelector(nameof(Cmd), nameof(CmdTypeSelector))]
        public object Args { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("args"), ModelTypeSelector(nameof(Event), nameof(EventTypeSelector))]
        public Optional<ReadOnlyMemory<byte>> Data { get; set; }
        
        private static Dictionary<RpcEventType, Type> CmdTypeSelector => new Dictionary<RpcEventType, Type>()
        {
            [RpcCommand.Authorize] = typeof(AuthorizeParams),
            [RpcCommand.Authenticate] = typeof(AuthenticateParams),
        };
        private static Dictionary<RpcEventType, Type> EventTypeSelector => new Dictionary<RpcEventType, Type>()
        {
            [RpcEvent.Authorize] = typeof(AuthorizeResponse),
            [RpcEvent.Authenticate] = typeof(AuthenticateResponse)
        };
    }
}
