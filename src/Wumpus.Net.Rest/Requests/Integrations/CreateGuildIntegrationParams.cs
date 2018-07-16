using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#create-guild-integration-json-params </summary>
    public class CreateGuildIntegrationParams
    {
        /// <summary> The <see cref="Entities.Integration"/> id. </summary>
        [ModelProperty("id")]
        public Snowflake IntegrationId { get; }
        /// <summary> The <see cref="Entities.Integration"/> type. </summary>
        [ModelProperty("type")]
        public Utf8String Type { get; }

        public CreateGuildIntegrationParams(Snowflake integrationId, Utf8String type)
        {
            IntegrationId = integrationId;
            Type = type;
        }

        public void Validate()
        {
            Preconditions.NotZero(IntegrationId, nameof(IntegrationId));
            Preconditions.NotNullOrWhitespace(Type, nameof(Type));
        }
    }
}
