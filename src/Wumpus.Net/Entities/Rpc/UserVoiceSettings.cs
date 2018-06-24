
using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class UserVoiceSettings
    {
        /// <summary> xxx </summary>
        [ModelProperty("userId")]
        internal Snowflake UserId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("pan")]
        public Optional<Pan> Pan { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("volume")]
        public Optional<int> Volume { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("mute")]
        public Optional<bool> Mute { get; set; }
    }
}
