using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class CreateGuildBanParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("delete-message-days")]
        public Optional<int> DeleteMessageDays { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("reason")]
        public Optional<string> Reason { get; set; } //TODO: Why is this not documented?
    }
}
