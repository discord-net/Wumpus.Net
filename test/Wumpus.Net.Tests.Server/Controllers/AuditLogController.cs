#pragma warning disable CS1998

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        [HttpGet("guilds/{guildId}/audit-logs")]
        public async Task<IActionResult> GetGuildAuditLogAsync(Snowflake guildId)
        {
            return BadRequest();
        }
    }
}