using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class Invite
    {
        /// <summary> xxx </summary>
        [ModelProperty("code")]
        public Utf8String Code { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("guild")]
        public InviteGuild Guild { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("channel")]
        public InviteChannel Channel { get; set; }
    }
}
