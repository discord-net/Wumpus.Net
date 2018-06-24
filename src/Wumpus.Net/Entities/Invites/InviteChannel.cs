using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class InviteChannel
    {
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        [ModelProperty("type")]
        public Utf8String Type { get; set; }
    }
}
