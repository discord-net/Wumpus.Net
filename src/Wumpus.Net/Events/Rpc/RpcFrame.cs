using Voltaic.Serialization;
using System;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class RpcFrame
    {
        /// <summary> xxx </summary>
        [ModelProperty("cmd")]
        public string Cmd { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("nonce")]
        public Optional<Guid?> Nonce { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("evt")]
        public Optional<string> Event { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("data")]
        public Optional<ReadOnlyBuffer<byte>> Data { get; set; }

        /// <summary> xxx </summary>
        [ModelProperty("args")]
        [ModelSelector(ModelSelectorGroups.RpcFrame, nameof(Event))]
        public object Args { get; set; }
    }
}
