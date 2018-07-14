#pragma warning disable CS1998

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class GatewayController : ControllerBase
    {
        // Gateway

        [HttpGet("gateway")]
        public async Task<IActionResult> GetGatewayAsync()
        {
            return BadRequest();
        }
        [HttpGet("gateway/bot")]
        public async Task<IActionResult> GetBotGatewayAsync()
        {
            return BadRequest();
        }
    }
}
