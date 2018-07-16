#pragma warning disable CS1998

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voltaic;
using Wumpus.Entities;
using Wumpus.Requests;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class InviteController : ControllerBase
    {
        [HttpGet("invites/{code}")]
        public async Task<IActionResult> GetInviteAsync(Utf8String code, [FromQuery] Dictionary<string, string> queryMap)
        {
            var args = new GetInviteParams();
            args.LoadQueryMap(queryMap);
            args.Validate();

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
