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

        public override IDictionary<string, string> CreateQueryMap()
        {
            var map = new Dictionary<string, string>();
            if (DeleteMessageDays.IsSpecified)
                map["delete-message-days"] = DeleteMessageDays.Value.ToString();
            if (Reason.IsSpecified)
                map["reason"] = Reason.Value.ToString();
            return map;
        }
        public void LoadQueryMap(IReadOnlyDictionary<string, string> map)
        {
            if (map.TryGetValue("delete-message-days", out string str))
                DeleteMessageDays = int.Parse(str);
            if (map.TryGetValue("reason", out str))
                Reason = (Utf8String)str;
        }

        public void Validate()
        {
            Preconditions.AtLeast(DeleteMessageDays, Ban.MinMessagePruneDays, nameof(DeleteMessageDays));
            Preconditions.AtMost(DeleteMessageDays, Ban.MaxMessagePruneDays, nameof(DeleteMessageDays));

            Preconditions.NotNullOrWhitespace(Reason, nameof(Reason));
        }
    }
}
