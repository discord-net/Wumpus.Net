using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class SetLocalVolumeResponse
    {
        /// <summary> xxx </summary>
        [ModelProperty("user_id")]
        public Snowflake UserId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("volume")]
        public int Volume { get; set; }
    }
}
