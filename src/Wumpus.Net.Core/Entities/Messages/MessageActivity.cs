using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#message-object-message-activity-structure </summary>
    public class MessageActivity
    {
        /// <summary> Type of <see cref="MessageActivityType"/>. </summary>
        [ModelProperty("type")]
        public MessageActivityType Type { get; set; }        
        /// <summary> Party id from a Rich Presence event. </summary>
        [ModelProperty("party_id")]
        public Optional<Utf8String> PartyId { get; set; }
    }
}
