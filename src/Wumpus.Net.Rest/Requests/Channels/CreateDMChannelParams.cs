using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/user#create-dm-json-params </summary>
    public class CreateDMChannelParams
    {
        /// <summary> The recipient to open a DM <see cref="Entities.Channel"/> with. </summary>
        [ModelProperty("recipient_id")]
        public Snowflake RecipientId { get; private set; }

        public CreateDMChannelParams(Snowflake recipientId)
        {
            RecipientId = recipientId;
        }

        public void Validate() { }
    }
}
