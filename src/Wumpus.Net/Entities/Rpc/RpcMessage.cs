using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class RpcMessage : Message
    {
        [ModelProperty("blocked")]
        public Optional<bool> IsBlocked { get; }
        [ModelProperty("content_parsed")]
        public Optional<object[]> ContentParsed { get; }
        [ModelProperty("author_color")]
        public Optional<string> AuthorColor { get; } //#Hex

        [ModelProperty("mentions")]
        public new Optional<ulong[]> UserMentions { get; set; }
    }
}
