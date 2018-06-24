using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class CreateDMChannelParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("recipient_id")]
        public ulong RecipientId { get; }

        public CreateDMChannelParams(ulong recipientId)
        {
            RecipientId = recipientId;
        }

        public void Validate() { }
    }
}
