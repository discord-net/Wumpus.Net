using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> 
    ///    Along with the HTTP error code, the API can also return more detailed error codes through a code key in the JSON error response. The response will also contain a message key containing a more friendly error string.
    ///    https://discordapp.com/developers/docs/topics/opcodes-and-status-codes#json
    /// </summary>
    public class RestError
    {
        /// <summary> The JSON error code for this <see cref="RestError"/>. </summary>
        [ModelProperty("code")]
        public int? Code { get; set; }
        /// <summary> The meaning of the error code for this <see cref="RestError"/>. </summary>
        [ModelProperty("message")]
        public Utf8String Message { get; set; }
    }
}
