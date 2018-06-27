using System;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class DeleteMessagesParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("messages")]
        public Snowflake[] MessageIds { get; }

        public DeleteMessagesParams(Snowflake[] messageIds)
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
