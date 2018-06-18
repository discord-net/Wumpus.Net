using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class CreateGuildIntegrationParams
    {
        [ModelProperty("id")]
        public ulong Id { get; }
        [ModelProperty("type")]
        public string Type { get; }

        public CreateGuildIntegrationParams(ulong id, string type)
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
