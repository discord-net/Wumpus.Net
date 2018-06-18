using System;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class DeleteMessagesParams
    {
        [ModelProperty("messages")]
        public ulong[] MessageIds { get; }

        public DeleteMessagesParams(ulong[] messageIds)
        {
            MessageIds = messageIds;
        }

        public void Validate()
        {
            Preconditions.NotNull(MessageIds, nameof(MessageIds));
            Preconditions.YoungerThan(MessageIds, TimeSpan.FromDays(14), nameof(MessageIds));
        }
    }
}
