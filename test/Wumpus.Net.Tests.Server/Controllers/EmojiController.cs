#pragma warning disable CS1998

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wumpus.Entities;
using Wumpus.Requests;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class EmojiController : ControllerBase
    {
        [HttpGet("guilds/{guildId}/emojis")]
        public async Task<IActionResult> GetGuildEmojisAsync(Snowflake guildId)
        {
            return Ok(new[] { new Emoji() });
        }
        [HttpGet("guilds/{guildId}/emoji/{emojiId}")]
        public async Task<IActionResult> GetGuildEmojiAsync(Snowflake guildId, Snowflake emojiId)
        {
            return Ok(new Emoji
            {
                Id = emojiId
            });
        }
        [HttpPost("guilds/{guildId}/emojis")]
        public async Task<IActionResult> CreateGuildEmojiAsync(Snowflake guildId, [FromBody] CreateGuildEmojiParams args)
        {
            args.Validate();

            return Ok(new Emoji
            {
                Name = args.Name,
                RoleIds = args.RoleIds
            });
        }
        [HttpPatch("guilds/{guildId}/emoji/{emojiId}")]
        public async Task<IActionResult> ModifyGuildEmojiAsync(Snowflake guildId, Snowflake emojiId, [FromBody] ModifyGuildEmojiParams args)
        {
            args.Validate();

            var emoji = new Emoji
            {
                Id = emojiId,
            };
            if (args.Name.IsSpecified)
                emoji.Name = args.Name.Value;
            if (args.RoleIds.IsSpecified)
                emoji.RoleIds = args.RoleIds.Value;
            return Ok(emoji);
        }
        [HttpPatch("guilds/{guildId}/emoji/{emojiId}")]
        public async Task<IActionResult> DeleteGuildEmojiAsync(Snowflake guildId, Snowflake emojiId)
        {
            return NoContent();
        }
    }
}
