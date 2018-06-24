using Voltaic.Serialization;
using System.Collections.Generic;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class MessageDeleteBulkEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public ulong ChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("ids")]
        public ulong[] Ids { get; set; }
    }
}
