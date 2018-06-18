using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class ModifyGuildRolePositionParams
    {
        [ModelProperty("id")]
        public ulong Id { get; }
        [ModelProperty("position")]
        public int Position { get; }

        public ModifyGuildRolePositionParams(ulong id, int position)
        {
            Id = id;
            Position = position;
        }

        public void Validate()
        {
            Preconditions.NotNegative(Position, nameof(Position));
        }
    }
}
