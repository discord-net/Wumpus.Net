using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class Invite
    {
        [ModelProperty("code")]
        public string Code { get; set; }
        [ModelProperty("guild")]
        public InviteGuild Guild { get; set; }
        [ModelProperty("channel")]
        public InviteChannel Channel { get; set; }
    }
}
