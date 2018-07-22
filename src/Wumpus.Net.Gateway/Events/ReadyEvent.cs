using Wumpus.Entities;
using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Events
{
    /// <summary> 
    ///     The ready event is dispatched when a client has completed the initial handshake with the gateway (for new sessions). 
    ///     The ready event can be the largest and most complex event the gateway will send, as it contains all the state required for a client to begin interacting with the rest of the platform.
    ///     https://discordapp.com/developers/docs/topics/gateway#ready
    /// </summary>
    [IgnorePropertiesAttribute("presences", "relationships", "user_settings")]
    public class ReadyEvent
    {
        /// <summary> Gateway protcol version. </summary>
        [ModelProperty("v")]
        public int Version { get; set; }
        /// <summary> Information about the <see cref="Entities.User"/> including email. </summary>
        [ModelProperty("user")]
        public User User { get; set; }
        /// <summary> Used for resuming connections. </summary>
        [ModelProperty("session_id")]
        public Utf8String SessionId { get; set; }
        /// <summary> The <see cref="Entities.Guild"/>s the <see cref="Entities.User"/> is in. </summary>
        [ModelProperty("guilds")]
        public UnavailableGuild[] Guilds { get; set; }
        /// <summary> The direct message channels the <see cref="Entities.User"/> is in. </summary>
        [ModelProperty("private_channels")]
        public Channel[] PrivateChannels { get; set; }
        /// <summary> Used for debugging - the <see cref="Entities.Guild"/>s the <see cref="Entities.User"/> is in. </summary>
        [ModelProperty("_trace")]
        public Utf8String[] Trace { get; set; }
    }
}
