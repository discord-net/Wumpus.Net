#pragma warning disable CS1998

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wumpus.Entities;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class VoiceController : ControllerBase
    {
        [HttpGet("voice/regions")]
        public async Task<IActionResult> GetVoiceRegionsAsync()
        {
            return Ok(new VoiceRegion());
        }
    }
}
