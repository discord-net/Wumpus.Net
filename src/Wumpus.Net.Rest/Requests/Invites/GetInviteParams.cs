using System.Collections.Generic;
using Voltaic;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/invite#get-invite-get-invite-url-parameters </summary>
    public class GetInviteParams : QueryMap
    {
        /// <summary> Whether the <see cref="Entities.Invite"/> should contain approximate member counts. </summary>
        public Optional<bool> WithCounts { get; set; }

        public override IDictionary<string, object> GetQueryMap()
        {
            var dict = new Dictionary<string, object>();
            if (WithCounts.IsSpecified)
                dict["with_counts"] = WithCounts.Value;
            return dict;
        }

        public void Validate() { }
    }
}
