using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class ModifyGuildChannelPositionParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; }
        /// <summary> xxx </summary>
        [ModelProperty("position")]
        public int Position { get; }

        public ModifyGuildChannelPositionParams(Snowflake id, int position)
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
