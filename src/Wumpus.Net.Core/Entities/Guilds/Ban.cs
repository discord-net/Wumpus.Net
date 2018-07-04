using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#ban-object </summary>
    public class Ban
    {
        public const int MinMessagePruneDays = 0;
        public const int MaxMessagePruneDays = 7;

        /// <summary> The reason for the <see cref="Ban"/>. </summary>
        [ModelProperty("reason")]
        public Utf8String Reason { get; set; }
        /// <summary> The banned <see cref="Entities.User"/>. </summary>
        [ModelProperty("user")]
        public User User { get; set; }
    }
}
