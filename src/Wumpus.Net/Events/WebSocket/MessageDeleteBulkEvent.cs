using Voltaic.Serialization;
using System.Collections.Generic;

namespace Wumpus.Events
{
    public class MessageDeleteBulkEvent
    {
        [ModelProperty("channel_id")]
        public ulong ChannelId { get; set; }
        [ModelProperty("ids")]
        public ulong[] Ids { get; set; }
    }
}
