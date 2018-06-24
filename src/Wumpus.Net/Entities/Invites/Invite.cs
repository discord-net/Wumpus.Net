using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class Invite
    {
        [ModelProperty("code")]
        public Utf8String Code { get; set; }
        [ModelProperty("guild")]
        public InviteGuild Guild { get; set; }
        [ModelProperty("channel")]
        public InviteChannel Channel { get; set; }
    }
}
