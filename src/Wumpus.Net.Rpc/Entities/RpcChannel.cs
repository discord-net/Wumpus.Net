using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class RpcChannel : Channel
    {
        /// <summary> xxx </summary>
        [ModelProperty("voice_states")]
        public VoiceState[] VoiceStates { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("messages")]
        public Message[] Messages { get; set; }
    }
}
