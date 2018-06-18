using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class ErrorEvent
    {
        [ModelProperty("code")]
        public int Code { get; set; }
        [ModelProperty("message")]
        public string Message { get; set; }
    }
}
