using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class RpcGuildMember
    {
        /// <summary> xxx </summary>
        [ModelProperty("user")]
        public User User { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("status")]
        public UserStatus Status { get; set; }
        /*[ModelProperty("activity")]
        public object Activity { get; set; }*/
    }
}
