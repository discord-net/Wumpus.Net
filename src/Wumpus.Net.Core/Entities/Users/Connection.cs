using Voltaic.Serialization;
using Voltaic;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/user#connection-object </summary>
    public class Connection
    {
        /// <summary> Id of the <see cref="Connection"/> account. </summary>
        [ModelProperty("id")]
        public Utf8String Id { get; set; }
        /// <summary> The service of the <see cref="Connection"/>. </summary>
        /// <remarks> Twitch, YouTube, etc. </remarks>
        [ModelProperty("type")]
        public Utf8String Type { get; set; }
        /// <summary> The username of the <see cref="Connection"/> account. </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> Whether the <see cref="Connection"/> is revoked. </summary>
        [ModelProperty("revoked")]
        public bool Revoked { get; set; }
        /// <summary> An array of partial <see cref="Integration"/>s. </summary>
        [ModelProperty("integrations")]
        public Snowflake[] Integrations { get; set; }
    }
}
