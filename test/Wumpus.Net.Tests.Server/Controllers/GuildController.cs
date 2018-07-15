#pragma warning disable CS1998

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wumpus.Requests;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class GuildController : ControllerBase
    {
        [HttpGet("guilds/{guildId}")]
        public async Task<IActionResult> GetGuildAsync(Snowflake guildId)
        {
            return BadRequest();
        }
        [HttpPost("guilds")]
        public async Task<IActionResult> CreateGuildAsync([FromBody] CreateGuildParams args)
        {
            return BadRequest();
        }
        [HttpPatch("guilds/{guildId}")]
        public async Task<IActionResult> ModifyGuildAsync(Snowflake guildId, [FromBody] ModifyGuildParams args)
        {
            return BadRequest();
        }
        [HttpDelete("guilds/{guildId}")]
        public async Task<IActionResult> DeleteGuildAsync(Snowflake guildId)
        {
            return BadRequest();
        }


        [HttpGet("guilds/{guildId}/channels")]
        public async Task<IActionResult> GetGuildChannelsAsync(Snowflake guildId)
        {
            return BadRequest();
        }
        [HttpPost("guilds/{guildId}/channels")]
        public async Task<IActionResult> CreateTextChannelAsync(Snowflake guildId, [FromBody] CreateTextChannelParams args)
        {
            return BadRequest();
        }
        [HttpPost("guilds/{guildId}/channels")]
        public async Task<IActionResult> CreateVoiceChannelAsync(Snowflake guildId, [FromBody] CreateVoiceChannelParams args)
        {
            return BadRequest();
        }
        [HttpPatch("guilds/{guildId}/channels")]
        public async Task<IActionResult> ModifyGuildChannelPositionsAsync(Snowflake guildId, [FromBody] IEnumerable<ModifyGuildChannelPositionParams> args)
        {
            return BadRequest();
        }


        [HttpGet("guilds/{guildId}/members")]
        public async Task<IActionResult> GetGuildMembersAsync(Snowflake guildId, GetGuildMembersParams args)
        {
            return BadRequest();
        }
        [HttpGet("guilds/{guildId}/members/{userId}")]
        public async Task<IActionResult> GetGuildMemberAsync(Snowflake guildId, Snowflake userId)
        {
            return BadRequest();
        }
        [HttpPut("guilds/{guildId}/members/{userId}")]
        public async Task<IActionResult> AddGuildMemberAsync(Snowflake guildId, Snowflake userId, [FromBody] CreateGuildEmojiParams args)
        {
            return BadRequest();
        }
        [HttpDelete("guilds/{guildId}/members/{userId}")]
        public async Task<IActionResult> RemoveGuildMemberAsync(Snowflake guildId, Snowflake userId)
        {
            return BadRequest();
        }
        [HttpPatch("guilds/{guildId}/members/{userId}")]
        public async Task<IActionResult> ModifyGuildMemberAsync(Snowflake guildId, Snowflake userId, [FromBody] ModifyGuildMemberParams args)
        {
            return BadRequest();
        }
        [HttpPatch("guilds/{guildId}/members/@me/nick")]
        public async Task<IActionResult> ModifyCurrentUserNickAsync(Snowflake guildId, [FromBody] ModifyCurrentUserNickParams args)
        {
            return BadRequest();
        }

        [HttpPut("guilds/{guildId}/members/{userId}/roles/{roleId}")]
        public async Task<IActionResult> AddGuildMemberRoleAsync(Snowflake guildId, Snowflake userId, Snowflake roleId)
        {
            return BadRequest();
        }
        [HttpDelete("guilds/{guildId}/members/{userId}/roles/{roleId}")]
        public async Task<IActionResult> RemoveGuildMemberRoleAsync(Snowflake guildId, Snowflake userId, Snowflake roleId)
        {
            return BadRequest();
        }

        [HttpGet("guilds/{guildId}/bans")]
        public async Task<IActionResult> GetGuildBansAsync(Snowflake guildId)
        {
            return BadRequest();
        }
        [HttpPut("guilds/{guildId}/bans/{userId}")]
        public async Task<IActionResult> AddGuildBansAsync(Snowflake guildId, Snowflake userId)
        {
            return BadRequest();
        }
        [HttpDelete("guilds/{guildId}/bans/{userId}")]
        public async Task<IActionResult> RemoveGuildBansAsync(Snowflake guildId, Snowflake userId)
        {
            return BadRequest();
        }

        [HttpGet("guilds/{guildId}/roles")]
        public async Task<IActionResult> GetGuildRolesAsync(Snowflake guildId)
        {
            return BadRequest();
        }
        [HttpPost("guilds/{guildId}/roles")]
        public async Task<IActionResult> CreateGuildRoleAsync(Snowflake guildId, [FromBody] CreateGuildRoleParams args)
        {
            return BadRequest();
        }
        [HttpDelete("guilds/{guildId}/roles/{roleId}")]
        public async Task<IActionResult> DeleteGuildRoleAsync(Snowflake guildId, Snowflake roleId)
        {
            return BadRequest();
        }
        [HttpPatch("guilds/{guildId}/roles")]
        public async Task<IActionResult> ModifyGuildRolePositionsAsync(Snowflake guildId, [FromBody] IEnumerable<ModifyGuildRolePositionParams> args)
        {
            return BadRequest();
        }
        [HttpPatch("guilds/{guildId}/roles/{roleId}")]
        public async Task<IActionResult> ModifyGuildRoleAsync(Snowflake guildId, Snowflake roleId, [FromBody] ModifyGuildRoleParams args)
        {
            return BadRequest();
        }

        [HttpGet("guilds/{guildId}/prune")]
        public async Task<IActionResult> GetGuildPruneCountAsync(Snowflake guildId, GuildPruneParams args)
        {
            return BadRequest();
        }

        [HttpPost("guilds/{guildId}/prune")]
        public async Task<IActionResult> PruneGuildMembersAsync(Snowflake guildId, GuildPruneParams args)
        {
            return BadRequest();
        }

        [HttpGet("guilds/{guildId}/regions")]
        public async Task<IActionResult> GetGuildVoiceRegionsAsync(Snowflake guildId)
        {
            return BadRequest();
        }

        [HttpGet("guilds/{guildId}/invites")]
        public async Task<IActionResult> GetGuildInvitesAsync(Snowflake guildId)
        {
            return BadRequest();
        }

        [HttpGet("guilds/{guildId}/integrations")]
        public async Task<IActionResult> GetGuildIntegrationsAsync(Snowflake guildId)
        {
            return BadRequest();
        }
        [HttpPost("guilds/{guildId}/integrations")]
        public async Task<IActionResult> CreateGuildIntegrationsAsync(Snowflake guildId, [FromBody] CreateGuildIntegrationParams args)
        {
            return BadRequest();
        }
        [HttpDelete("guilds/{guildId}/integrations/{integrationId}")]
        public async Task<IActionResult> DeleteGuildIntegrationsAsync(Snowflake guildId, Snowflake integrationId)
        {
            return BadRequest();
        }
        [HttpPatch("guilds/{guildId}/integrations/{integrationId}")]
        public async Task<IActionResult> ModifyGuildIntegrationsAsync(Snowflake guildId, Snowflake integrationId, [FromBody] ModifyGuildIntegrationParams args)
        {
            return BadRequest();
        }
        [HttpPost("guilds/{guildId}/integrations/{integrationId}/sync")]
        public async Task<IActionResult> SyncGuildIntegrationsAsync(Snowflake guildId, Snowflake integrationId)
        {
            return BadRequest();
        }

        [HttpGet("guilds/{guildId}/embed")]
        public async Task<IActionResult> GetGuildEmbedAsync(Snowflake guildId)
        {
            return BadRequest();
        }
        [HttpPatch("guilds/{guildId}/embed")]
        public async Task<IActionResult> ModifyGuildEmbedAsync(Snowflake guildId, [FromBody] ModifyGuildEmbedParams args)
        {
            return BadRequest();
        }
    }
}
