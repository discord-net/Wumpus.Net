using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class CreateGuildIntegrationParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; }
        /// <summary> xxx </summary>
        [ModelProperty("type")]
        public string Type { get; }

        public CreateGuildIntegrationParams(Snowflake id, string type)
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
