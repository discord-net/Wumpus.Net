using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class CreateDMChannelParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("recipient_id")]
        public Snowflake RecipientId { get; }

        public CreateDMChannelParams(Snowflake recipientId)
        {
            RecipientId = recipientId;
        }

        public void Validate() { }
    }
}
