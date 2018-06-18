using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    public class CreateGuildBanParams
    {
        [ModelProperty("delete-message-days")]
        public Optional<int> DeleteMessageDays { get; set; }
        [ModelProperty("reason")]
        public Optional<string> Reason { get; set; } //TODO: Why is this not documented?
    }
}
