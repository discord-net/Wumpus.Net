#pragma warning disable CS1998

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Voltaic;
using Wumpus.Entities;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class VoiceController : ControllerBase
    {
        [HttpGet("voice/regions")]
        public async Task<IActionResult> GetVoiceRegionsAsync()
        {
            return Ok(new VoiceRegion[]
            {
                new VoiceRegion
                {
                    Id = (Utf8String)"test",
                    Name = (Utf8String)"Test",
                    IsOptimal = false,
                    IsVip = false,
                    Deprecated = false,
                    Custom = true,
                },
                new VoiceRegion
                {
                    Id = (Utf8String)"test-wip",
                    Name = (Utf8String)"Test (VIP)",
                    IsOptimal = false,
                    IsVip = true,
                    Deprecated = false,
                    Custom = true,
                }
            });
        }
    }
}
