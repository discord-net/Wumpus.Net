#pragma warning disable CS1998

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wumpus.Requests;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        // User

        [HttpGet("users/@me")]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            return BadRequest();
        }
        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUserAsync(Snowflake userId)
        {
            return BadRequest();
        }
        [HttpPatch("users/@me")]
        public async Task<IActionResult> ModifyCurrentUserAsync([FromBody] ModifyCurrentUserParams args)
        {
            return BadRequest();
        }

        [HttpGet("users/@me/guilds")]
        public async Task<IActionResult> GetCurrentUserGuildsAsync(GetCurrentUserGuildsParams args)
        {
            return BadRequest();
        }
        [HttpDelete("users/@me/guilds/{guildId}")]
        public async Task<IActionResult> LeaveGuildAsync(Snowflake guildId)
        {
            return BadRequest();
        }

        [HttpGet("users/@me/channels")]
        public async Task<IActionResult> GetPrivateChannelsAsync()
        {
            return BadRequest();
        }
        [HttpPost("users/@me/channels")]
        public async Task<IActionResult> CreateDMChannelAsync([FromBody] CreateDMChannelParams args)
        {
            return BadRequest();
        }
        [HttpPost("users/@me/channels")]
        public async Task<IActionResult> CreateGroupChannelAsync([FromBody] CreateGroupChannelParams args)
        {
            return BadRequest();
        }

        [HttpGet("users/@me/connections")]
        public async Task<IActionResult> GetUserConnectionsAsync()
        {
            return BadRequest();
        }
    }
}
