#pragma warning disable CS1998

using System.Collections.Generic;
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
        public async Task<IActionResult> GetGuildAuditLogAsync(Snowflake guildId, [FromQuery] Dictionary<string, string> queryMap)
        {
            var args = new GetGuildAuditLogParams();
            args.LoadQueryMap(queryMap);
            args.Validate();

            var entry = new AuditLogEntry
            {
                ActionType = args.ActionType.GetValueOrDefault(AuditLogEvent.ChannelCreate)
            };
            if (args.Before.IsSpecified)
                entry.Id = args.Before.Value;
            if (args.UserId.IsSpecified)
                entry.UserId = args.UserId.Value;

            return Ok(new AuditLog
            {
                Entries = new[] { entry }
            });
        }
    }
}