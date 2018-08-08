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
        public RpcCommand Command { get; set; }
        /// <summary> Subscription event </summary>
        [ModelProperty("evt")]
        public Optional<RpcEvent> Event { get; set; } 
        /// <summary> Unique string used once for replies from the server	 </summary>
        [ModelProperty("nonce")]
        public Optional<Guid?> Nonce { get; set; }        

        /// <summary> xxx </summary>
        [ModelProperty("args"), ModelTypeSelector(nameof(Command), nameof(CommandTypeSelector))]
        public object Args { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("args"), ModelTypeSelector(nameof(Event), nameof(EventTypeSelector))]
        public Optional<object> Data { get; set; }
        
        private static Dictionary<RpcCommand, Type> CommandTypeSelector => new Dictionary<RpcCommand, Type>()
        {
        };
        private static Dictionary<RpcEvent, Type> EventTypeSelector => new Dictionary<RpcEvent, Type>()
        {
        };
    }
}
