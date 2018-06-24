using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class GetChannelParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public ulong ChannelId { get; set; }
    }
}
