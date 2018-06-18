using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class RpcGuildMember
    {
        [ModelProperty("user")]
        public User User { get; set; }
        [ModelProperty("status")]
        public UserStatus Status { get; set; }
        /*[ModelProperty("activity")]
        public object Activity { get; set; }*/
    }
}
