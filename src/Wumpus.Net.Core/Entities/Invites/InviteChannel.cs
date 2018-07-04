using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/invite#invite-object-example-invite-object </summary>
    public class InviteChannel
    {
        /// <summary> The id of the <see cref="InviteChannel"/>. </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> The name of the <see cref="InviteChannel"/>. </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> The type of the <see cref="InviteChannel"/> </summary>
        [ModelProperty("type")]
        public ChannelType Type { get; set; }
    }
}
