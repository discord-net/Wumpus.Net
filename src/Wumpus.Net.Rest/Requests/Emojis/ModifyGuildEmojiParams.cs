using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/emoji#modify-guild-emoji-json-params </summary>
    public class ModifyGuildEmojiParams
    {
        /// <summary> Name of the <see cref="Entities.Emoji"/>. </summary>
        [ModelProperty("name")]
        public Optional<Utf8String> Name { get; set; }
        /// <summary> The 128x128 emoji image. </summary>
        [ModelProperty("roles")]
        public Optional<Snowflake[]> RoleIds { get; set; }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Name, nameof(Name));
        }
    }
}
