using System.Collections.Generic;
using Voltaic;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/invite#get-invite-get-invite-url-parameters </summary>
    public class GetInviteParams : QueryMap
    {
        /// <summary> Whether the <see cref="Entities.Invite"/> should contain approximate member counts. </summary>
        public Optional<bool> WithCounts { get; set; }

        public override IDictionary<string, string> CreateQueryMap()
        {
            var map = new Dictionary<string, string>();
            if (WithCounts.IsSpecified)
                map["with_counts"] = WithCounts.Value.ToString();
            return map;
        }
        public void LoadQueryMap(IReadOnlyDictionary<string, string> map)
        {
            if (map.TryGetValue("with_counts", out string str))
                WithCounts = bool.Parse(str);
        }

        public void Validate() { }
    }
}
