using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#modify-guild-role-positions-json-params </summary>
    public class ModifyGuildRolePositionParams
    {
        /// <summary> <see cref="Entities.Role"/>. </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; private set; }
        /// <summary> Sorting position of the <see cref="Entities.Role"/>. </summary>
        [ModelProperty("position")]
        public int Position { get; private set; }

        public ModifyGuildRolePositionParams(Snowflake id, int position)
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
