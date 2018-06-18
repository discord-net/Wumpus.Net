using Voltaic.Serialization;
using System;
using Voltaic;

namespace Wumpus.Entities
{
    public class Message
    {
        [ModelProperty("id")]
        public ulong Id { get; set; }
        [ModelProperty("channel_id")]
        public ulong ChannelId { get; set; }
        [ModelProperty("type")]
        public MessageType Type { get; set; }
        [ModelProperty("author")]
        public Optional<User> Author { get; set; }
        [ModelProperty("content")]
        public Optional<string> Content { get; set; }
        [ModelProperty("timestamp")]
        public Optional<DateTimeOffset> Timestamp { get; set; }
        [ModelProperty("edited_timestamp")]
        public Optional<DateTimeOffset?> EditedTimestamp { get; set; }
        [ModelProperty("tts")]
        public Optional<bool> IsTextToSpeech { get; set; }
        [ModelProperty("mention_everyone")]
        public Optional<bool> MentionEveryone { get; set; }
        [ModelProperty("mentions")]
        public Optional<EntityOrId<User>[]> UserMentions { get; set; }
        [ModelProperty("mention_roles")]
        public Optional<ulong[]> RoleMentions { get; set; }
        [ModelProperty("attachments")]
        public Optional<Attachment[]> Attachments { get; set; }
        [ModelProperty("embeds")]
        public Optional<Embed[]> Embeds { get; set; }
        [ModelProperty("reactions")]
        public Optional<Reaction[]> Reactions { get; set; }
        [ModelProperty("nonce")]
        public Optional<string> Nonce { get; set; }
        [ModelProperty("pinned")]
        public Optional<bool> Pinned { get; set; }
        [ModelProperty("webhook_id")]
        public Optional<ulong> WebhookId { get; set; }
    }
}
