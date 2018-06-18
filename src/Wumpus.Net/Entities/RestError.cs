using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class RestError
    {
        [ModelProperty("code")]
        public int? Code { get; set; }
        [ModelProperty("message")]
        public string Message { get; set; }
    }
}
