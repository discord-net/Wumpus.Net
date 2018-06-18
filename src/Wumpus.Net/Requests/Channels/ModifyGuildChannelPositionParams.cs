using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class ModifyGuildChannelPositionParams
    {
        [ModelProperty("id")]
        public ulong Id { get; }
        [ModelProperty("position")]
        public int Position { get; }

        public ModifyGuildChannelPositionParams(ulong id, int position)
        {
            Id = id;
            Position = position;
        }

        public void Validate()
        {
            Preconditions.NotZero(Id, nameof(Id));
            Preconditions.NotNegative(Position, nameof(Position));
        }
    }
}
