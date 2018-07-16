using System.Collections.Generic;
using Voltaic;
using Voltaic.Serialization;
using Wumpus.Entities;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#create-guild-ban-query-string-params </summary>
    public class CreateGuildBanParams : QueryMap
    {
        /// <summary> Number of days to delete <see cref="Message"/>s for. </summary>
        [ModelProperty("delete-message-days")]
        public Optional<int> DeleteMessageDays { get; set; }
        /// <summary> Reason for the <see cref="Ban"/>. </summary>
        [ModelProperty("reason")]
        public Optional<Utf8String> Reason { get; set; }

        public override IDictionary<string, object> GetQueryMap()
        {
            var dict = new Dictionary<string, object>();
            if (DeleteMessageDays.IsSpecified)
                dict["delete-message-days"] = DeleteMessageDays.Value;
            if (Reason.IsSpecified)
                dict["reason"] = Reason.Value;
            return dict;
        }

        public void Validate()
        {
            Preconditions.AtLeast(DeleteMessageDays, Ban.MinMessagePruneDays, nameof(DeleteMessageDays));
            Preconditions.AtMost(DeleteMessageDays, Ban.MaxMessagePruneDays, nameof(DeleteMessageDays));

            Preconditions.NotNullOrWhitespace(Reason, nameof(Reason));
        }
    }
}
