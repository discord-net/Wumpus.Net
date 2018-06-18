using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class CreateDMChannelParams
    {
        [ModelProperty("recipient_id")]
        public ulong RecipientId { get; }

        public CreateDMChannelParams(ulong recipientId)
        {
            RecipientId = recipientId;
        }

        public void Validate() { }
    }
}
