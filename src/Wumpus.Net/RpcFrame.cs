using Voltaic.Serialization;
using System;
using Voltaic;
using System.Collections.Generic;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class RpcFrame
    {
        /// <summary> xxx </summary>
        [ModelProperty("cmd")]
        public Utf8String Cmd { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("nonce")]
        public Optional<Guid?> Nonce { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("evt")]
        public Optional<RpcEventType> Event { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("data")]
        public Optional<ReadOnlyMemory<byte>> Data { get; set; }

        /// <summary> xxx </summary>
        [ModelProperty("args")]
        [ModelTypeSelector(nameof(Event), nameof(EventTypeSelector))]
        public object Args { get; set; }
        
        private static Dictionary<RpcEventType, Type> EventTypeSelector => new Dictionary<RpcEventType, Type>()
        {
        };
    }
}
