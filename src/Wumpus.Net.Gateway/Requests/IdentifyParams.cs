using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/topics/gateway#identify </summary>
    public class IdentifyParams
    {
        /// <summary> authentication token </summary>
        [ModelProperty("token")]
        public Utf8String Token { get; set; }
        /// <summary> connection properties </summary>
        [ModelProperty("properties")]
        public IdentityConnectionProperties Properties { get; set; }
        /// <summary> whether this connection supports compression of packets </summary>
        [ModelProperty("compress")]
        public Optional<bool> Compress { get; set; }
        /// <summary> value between 50 and 250, total number of members where the gateway will stop sending offline members in the guild member list </summary>
        [ModelProperty("large_threshold")]
        public Optional<int> LargeThreshold { get; set; }
        /// <summary> used for Guild Sharding </summary>
        [ModelProperty("shard")]
        public Optional<int[]> Shard { get; set; }
        /// <summary> presence structure for initial presence information </summary>
        [ModelProperty("presence")]
        public Optional<UpdateStatusParams> Presence { get; set; }
    }

    /// <summary> https://discordapp.com/developers/docs/topics/gateway#identify-identify-connection-properties </summary>
    public class IdentityConnectionProperties
    {
        /// <summary> your operating system </summary>
        [ModelProperty("$os")]
        public Utf8String Os { get; set; }
        /// <summary> your library name </summary>
        [ModelProperty("$browser")]
        public Utf8String Browser { get; set; }
        /// <summary> your library name </summary>
        [ModelProperty("$device")]
        public Utf8String Device { get; set; }
    }
}
