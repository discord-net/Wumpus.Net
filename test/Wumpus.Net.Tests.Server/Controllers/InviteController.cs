#pragma warning disable CS1998

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Voltaic;
using Wumpus.Entities;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class InviteController : ControllerBase
    {
        [HttpGet("invites/{code}")]
        public async Task<IActionResult> GetInviteAsync(Utf8String code)
        {
            return Ok(new Invite
            {
                Code = code
            });
        }
        [HttpDelete("invites/{code}")]
        public async Task<IActionResult> DeleteInviteAsync(Utf8String code)
        {
            return Ok(new Invite
            {
                Code = code
            });
        }
    }
}
