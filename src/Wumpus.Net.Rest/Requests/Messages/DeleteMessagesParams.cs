using System;
using Voltaic.Serialization;
using Wumpus.Entities;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#bulk-delete-messages-json-params </summary>
    public class DeleteMessagesParams
    {
        /// <summary> An array of <see cref="Entities.Message"/> ids to delete. </summary>
        [ModelProperty("messages")]
        public Snowflake[] MessageIds { get; }

        public DeleteMessagesParams(Snowflake[] messageIds)
        {
            MessageIds = messageIds;
        }

        public void Validate()
        {
            Preconditions.NotNull(MessageIds, nameof(MessageIds));
            Preconditions.AtLeast(MessageIds.GetLength(0), Channel.MinBulkMessageDeleteAmount, nameof(MessageIds));
            Preconditions.AtMost(MessageIds.GetLength(0), Channel.MaxBulkMessageDeleteAmount, nameof(MessageIds));
            Preconditions.YoungerThan(MessageIds, TimeSpan.FromDays(14), nameof(MessageIds));
        }
    }
}
