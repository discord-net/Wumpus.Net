using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> 
    ///     Sent on connection to the websocket. Defines the heartbeat interval that the client should heartbeat to.
    ///     https://discordapp.com/developers/docs/topics/gateway#hello
    /// </summary>
    public class HelloEvent
    {
        /// <summary> The interval (in milliseconds) the client should heartbeat with. </summary>
        [ModelProperty("heartbeat_interval")]
        public int HeartbeatInterval { get; set; }
        /// <summary> Used for debugging, array of servers connected to.  </summary>
        [ModelProperty("_trace")]
        public Utf8String[] Trace { get; set; }
    }
}
