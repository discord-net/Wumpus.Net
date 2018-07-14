#pragma warning disable CS1998

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Voltaic;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class InviteCotroller : ControllerBase
    {
        // Invite

        [HttpGet("invites/{code}")]
        public async Task<IActionResult> GetInviteAsync(Utf8String code)
        {
            return BadRequest();
        }
        [HttpDelete("invites/{code}")]
        public async Task<IActionResult> DeleteInviteAsync(Utf8String code)
        {
            return BadRequest();
        }
        [HttpPost("invites/{code}")]
        public async Task<IActionResult> AcceptInviteAsync(Utf8String code)
        {
            return BadRequest();
        }
    }
}
