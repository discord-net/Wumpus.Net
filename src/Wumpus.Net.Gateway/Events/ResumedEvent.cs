using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> 
    ///     The resumed event is dispatched when a client has sent a <see cref="GatewayOperation.Resume"/> to the gateway (for resuming existing sessions).
    ///     https://discordapp.com/developers/docs/topics/gateway#resumed
    /// </summary>
    public class ResumedEvent
    {
        /// <summary> Used for debugging - the <see cref="Entities.Guild"/>s the <see cref="Entities.User"/> is in. </summary>
        [ModelProperty("_trace")]
        public Utf8String[] Trace { get; set; }
    }
}
