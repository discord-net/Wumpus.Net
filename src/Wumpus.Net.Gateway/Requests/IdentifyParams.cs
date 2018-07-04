using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary>
    ///     Used to trigger the initial handshake with the gateway.
    ///     https://discordapp.com/developers/docs/topics/gateway#identify 
    /// </summary>
    public class IdentifyParams
    {
        /// <summary> Authentication token. </summary>
        [ModelProperty("token")]
        public Utf8String Token { get; set; }
        /// <summary> Connection properties. </summary>
        [ModelProperty("properties")]
        public IdentityConnectionProperties Properties { get; set; }
        /// <summary> Whether this connection supports compression of packets. </summary>
        [ModelProperty("compress")]
        public Optional<bool> Compress { get; set; }
        /// <summary> Value between 50 and 250; total number of <see cref="Entities.GuildMember"/>s where the gateway will stop sending offline <see cref="Entities.GuildMember"/> in the <see cref="Entities.Guild" /> <see cref="Entities.GuildMember"/> list. </summary>
        [ModelProperty("large_threshold")]
        public Optional<int> LargeThreshold { get; set; }
        /// <summary> Used for <see cref="Entities.Guild"/> Sharding </summary>
        [ModelProperty("shard")]
        public Optional<int[]> Shard { get; set; }
        /// <summary> Presence structure for initial presence information. </summary>
        [ModelProperty("presence")]
        public Optional<UpdateStatusParams> Presence { get; set; }
    }

    /// <summary> https://discordapp.com/developers/docs/topics/gateway#identify-identify-connection-properties </summary>
    public class IdentityConnectionProperties
    {
        /// <summary> Your operating system. </summary>
        [ModelProperty("$os")]
        public Utf8String Os { get; set; }
        /// <summary> Your browser name. </summary>
        [ModelProperty("$browser")]
        public Utf8String Browser { get; set; }
        /// <summary> Your device name. </summary>
        [ModelProperty("$device")]
        public Utf8String Device { get; set; }
    }
}
