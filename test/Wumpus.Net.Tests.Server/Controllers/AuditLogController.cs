#pragma warning disable CS1998

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wumpus.Entities;
using Wumpus.Requests;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        [HttpGet("guilds/{guildId}/audit-logs")]
        public async Task<IActionResult> GetGuildAuditLogAsync(Snowflake guildId, [FromBody] GetGuildAuditLogParams args)
        {
            return Ok(new AuditLog
            {
                Entries = new[]
                {
                    new AuditLogEntry
                    {
                        ActionType = args.ActionType.Value,
                        Id = args.Before.Value,
                        UserId = args.UserId.Value
                    }
                }
            });
        }
    }
}