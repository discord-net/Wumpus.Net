using Voltaic.Serialization;
using System;
using Voltaic;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class Message
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("type")]
        public MessageType Type { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("author")]
        public Optional<User> Author { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("content")]
        public Optional<Utf8String> Content { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("timestamp")]
        [StandardFormat('O')]
        public Optional<DateTimeOffset> Timestamp { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("edited_timestamp")]
        [StandardFormat('O')]
        public Optional<DateTimeOffset?> EditedTimestamp { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("tts")]
        public Optional<bool> IsTextToSpeech { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("mention_everyone")]
        public Optional<bool> MentionEveryone { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("mentions")]
        public Optional<EntityOrId<User>[]> UserMentions { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("mention_roles")]
        public Optional<Snowflake[]> RoleMentions { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("attachments")]
        public Optional<Attachment[]> Attachments { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("embeds")]
        public Optional<Embed[]> Embeds { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("reactions")]
        public Optional<Reaction[]> Reactions { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("nonce")]
        public Optional<Utf8String> Nonce { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("pinned")]
        public Optional<bool> Pinned { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("webhook_id")]
        public Optional<Snowflake> WebhookId { get; set; }
    }
}
