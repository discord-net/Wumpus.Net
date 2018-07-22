using System;
using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class GuildMember : Member
    {
        /// <summary> <see cref="User"/> object. </summary>
        [ModelProperty("user")]
        public User User { get; set; }
    }
}
