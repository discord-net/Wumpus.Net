#pragma warning disable CS1998

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Voltaic;
using Wumpus.Responses;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class GatewayController : ControllerBase
    {
        [HttpGet("gateway")]
        public async Task<IActionResult> GetGatewayAsync()
        {
            return Ok(new GetGatewayResponse
            {
                Url = (Utf8String)Url.Content("")
            });
        }
        [HttpGet("gateway/bot")]
        public async Task<IActionResult> GetBotGatewayAsync()
        {
            return Ok(new GetBotGatewayResponse
            {
                Url = (Utf8String)Url.Content(""),
                Shards = 1
            });
        }
    }
}
