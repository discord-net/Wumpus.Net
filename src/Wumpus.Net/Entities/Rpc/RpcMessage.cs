using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class RpcMessage : Message
    {
        /// <summary> xxx </summary>
        [ModelProperty("blocked")]
        public Optional<bool> IsBlocked { get; }
        /// <summary> xxx </summary>
        [ModelProperty("content_parsed")]
        public Optional<object[]> ContentParsed { get; }
        /// <summary> xxx </summary>
        [ModelProperty("author_color")]
        public Optional<string> AuthorColor { get; } //#Hex

        /// <summary> xxx </summary>
        [ModelProperty("mentions")]
        public new Optional<ulong[]> UserMentions { get; set; }
    }
}
