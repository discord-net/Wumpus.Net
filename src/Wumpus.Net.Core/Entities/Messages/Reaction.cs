using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#reaction-object </summary>
    public class Reaction
    {
        /// <summary> Times this <see cref="Entities.Emoji"/> has been used to react. </summary>
        [ModelProperty("count")]
        public int Count { get; set; }
        /// <summary> Whether the current <see cref="User"/> reacted using this <see cref="Entities.Emoji"/>. </summary>
        [ModelProperty("me")]
        public bool Me { get; set; }
        /// <summary> <see cref="Entities.Emoji"/> information. </summary>
        [ModelProperty("emoji")]
        public Emoji Emoji { get; set; }
    }
}
