using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#create-guild-integration-json-params </summary>
    public class CreateGuildIntegrationParams
    {
        /// <summary> The <see cref="Entities.Integration"/> id. </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; }
        /// <summary> The <see cref="Entities.Integration"/> type. </summary>
        [ModelProperty("type")]
        public Utf8String Type { get; }

        public CreateGuildIntegrationParams(Snowflake id, Utf8String type)
        {
            Id = id;
            Type = type;
        }

        public void Validate()
        {
            Preconditions.NotZero(Id, nameof(Id));
            Preconditions.NotNullOrWhitespace(Type, nameof(Type));
        }
    }
}
