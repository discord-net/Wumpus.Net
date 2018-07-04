using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#integration-account-object </summary>
    public class IntegrationAccount
    {
        /// <summary> Id of the <see cref="IntegrationAccount"/>. </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> Name of the <see cref="IntegrationAccount"/>. </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
    }
}
