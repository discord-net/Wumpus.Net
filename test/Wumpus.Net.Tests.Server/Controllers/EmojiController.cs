#pragma warning disable CS1998

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wumpus.Requests;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class EmojiController : ControllerBase
    {
        [HttpGet("guilds/{guildId}/emojis")]
        public async Task<IActionResult> GetGuildEmojisAsync(Snowflake guildId)
        {
            return BadRequest();
        }
        [HttpGet("guilds/{guildId}/emoji/{emojiId}")]
        public async Task<IActionResult> GetGuildEmojiAsync(Snowflake guildId, Snowflake emojiId)
        {
            return BadRequest();
        }
        [HttpPost("guilds/{guildId}/emojis")]
        public async Task<IActionResult> CreateGuildEmojiAsync(Snowflake guildId, [FromBody] CreateGuildEmojiParams args)
        {
            return BadRequest();
        }
        [HttpPatch("guilds/{guildId}/emoji/{emojiId}")]
        public async Task<IActionResult> ModifyGuildEmojiAsync(Snowflake guildId, [FromBody] ModifyGuildEmojiParams args)
        {
            return BadRequest();
        }
        [HttpPatch("guilds/{guildId}/emoji/{emojiId}")]
        public async Task<IActionResult> DeleteGuildEmojiAsync(Snowflake guildId, Snowflake emojiId)
        {
            return NoContent();
        }
    }
}
