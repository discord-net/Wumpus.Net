#pragma warning disable CS1998

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wumpus.Entities;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class OAuthController : ControllerBase
    {
        [HttpGet("oauth2/applications/me")]
        public async Task<IActionResult> GetCurrentApplicationAsync()
        {
            return Ok(new Application());
        }
    }
}
