using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#modify-guild-channel-positions-json-params </summary>
    public class ModifyGuildChannelPositionParams
    {
        /// <summary> <see cref="Entities.Channel"/> id; </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; private set; }
        /// <summary> Sorting position of the <see cref="Entities.Channel"/>. </summary>
        [ModelProperty("position")]
        public int Position { get; private set; }

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
