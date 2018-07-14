#pragma warning disable CS1998

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class VoiceController : ControllerBase
    {
        // Voice

        [HttpGet("voice/regions")]
        public async Task<IActionResult> GetVoiceRegionsAsync()
        {
            return BadRequest();
        }
    }
}
