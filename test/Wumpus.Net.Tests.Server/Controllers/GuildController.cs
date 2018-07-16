#pragma warning disable CS1998

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Voltaic;
using Wumpus.Entities;
using Wumpus.Requests;
using Wumpus.Responses;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class GuildController : ControllerBase
    {
        [HttpGet("guilds/{guildId}")]
        public async Task<IActionResult> GetGuildAsync(Snowflake guildId)
        {
            return Ok(new Guild
            {
                Id = guildId
            });
        }
        [HttpPost("guilds")]
        public async Task<IActionResult> CreateGuildAsync([FromBody] CreateGuildParams args)
        {
            args.Validate();

            var guild = new Guild
            {
                DefaultMessageNotifications = args.DefaultMessageNotifications.GetValueOrDefault(DefaultMessageNotifications.AllMessages),
                ExplicitContentFilter = args.ExplicitContentFilter.GetValueOrDefault(ExplicitContentFilter.Disabled),
                Name = args.Name,
                Region = args.Region,
                VerificationLevel = args.VerificationLevel.GetValueOrDefault()
            };
            if (args.Icon.IsSpecified)
                guild.Icon = args.Icon.Value;
            if (args.Roles.IsSpecified)
            {
                guild.Roles = args.Roles.Value.Select(x =>
                    new Role
                    {
                        Color = x.Color.GetValueOrDefault(Color.Default),
                        IsHoisted = x.IsHoisted.GetValueOrDefault(false),
                        IsMentionable = x.IsMentionable.GetValueOrDefault(false),
                        Name = x.Name.GetValueOrDefault((Utf8String)"new role"),
                        Permissions = x.Permissions.GetValueOrDefault(GuildPermissions.None)
                    }
                ).ToArray();
            }
            return Ok(guild);
        }
        [HttpPatch("guilds/{guildId}")]
        public async Task<IActionResult> ModifyGuildAsync(Snowflake guildId, [FromBody] ModifyGuildParams args)
        {
            args.Validate();

            var guild = new Guild
            {
                Id = guildId
            };
            if (args.AfkChannelId.IsSpecified)
                guild.AfkChannelId = args.AfkChannelId.Value;
            if (args.AfkTimeout.IsSpecified)
                guild.AfkTimeout = args.AfkTimeout.Value;
            if (args.DefaultMessageNotifications.IsSpecified)
                guild.DefaultMessageNotifications = args.DefaultMessageNotifications.Value;
            if (args.ExplicitContentFilter.IsSpecified)
                guild.ExplicitContentFilter = args.ExplicitContentFilter.Value;
            if (args.Icon.IsSpecified)
                guild.Icon = args.Icon.Value;
            if (args.Name.IsSpecified)
                guild.Name = args.Name.Value;
            if (args.OwnerId.IsSpecified)
                guild.OwnerId = args.OwnerId.Value;
            if (args.Region.IsSpecified)
                guild.Region = args.Region.Value;
            if (args.Splash.IsSpecified)
                guild.Splash = args.Splash.Value;
            if (args.SystemChannelId.IsSpecified)
                guild.SystemChannelId = args.SystemChannelId.Value;
            if (args.VerificationLevel.IsSpecified)
                guild.VerificationLevel = args.VerificationLevel.Value;

            return Ok(guild);
        }
        [HttpDelete("guilds/{guildId}")]
        public async Task<IActionResult> DeleteGuildAsync(Snowflake guildId)
        {
            return NoContent();
        }

        [HttpGet("guilds/{guildId}/channels")]
        public async Task<IActionResult> GetGuildChannelsAsync(Snowflake guildId)
        {
            return Ok(new[] { new Channel() });
        }
        [HttpPost("guilds/{guildId}/channels")]
        public async Task<IActionResult> CreateTextChannelAsync(Snowflake guildId, [FromBody] CreateTextChannelParams args)
        {
            args.Validate();

            return Ok(new Channel
            {
                IsNsfw = args.IsNsfw,
                Name = args.Name,
                ParentId = args.ParentId,
                PermissionOverwrites = args.PermissionOverwrites,
                Type = args.Type.Value
            });
        }
        [HttpPost("guilds/{guildId}/channels")]
        public async Task<IActionResult> CreateVoiceChannelAsync(Snowflake guildId, [FromBody] CreateVoiceChannelParams args)
        {
            args.Validate();

            return Ok(new Channel
            {
                Bitrate = args.Bitrate,
                Name = args.Name,
                ParentId = args.ParentId,
                PermissionOverwrites = args.PermissionOverwrites,
                Type = args.Type.Value
            });
        }
        [HttpPatch("guilds/{guildId}/channels")]
        public async Task<IActionResult> ModifyGuildChannelPositionsAsync(Snowflake guildId, [FromBody] IEnumerable<ModifyGuildChannelPositionParams> args)
        {
            foreach (var arg in args)
                arg.Validate();

            return Ok(args.Select(x => new Channel
            {
                Id = x.Id,
                Position = x.Position
            }).ToArray());
        }

        [HttpGet("guilds/{guildId}/members")]
        public async Task<IActionResult> GetGuildMembersAsync(Snowflake guildId, [FromQuery] Dictionary<string, string> queryMap)
        {
            var args = new GetGuildMembersParams();
            args.LoadQueryMap(queryMap);
            args.Validate();

            var user = new User();
            if (args.After.IsSpecified)
                user.Id = args.After.Value;
            return Ok(new[] { new GuildMember
            {
                User = user
            }});
        }
        [HttpGet("guilds/{guildId}/members/{userId}")]
        public async Task<IActionResult> GetGuildMemberAsync(Snowflake guildId, Snowflake userId)
        {
            return Ok(new[] { new GuildMember
            {
                User = new User
                {
                    Id = userId
                }
            }});
        }
        [HttpPut("guilds/{guildId}/members/{userId}")]
        public async Task<IActionResult> AddGuildMemberAsync(Snowflake guildId, Snowflake userId, [FromBody] AddGuildMemberParams args)
        {
            args.Validate();

            return Ok(new GuildMember
            {
                User = new User
                {
                    Id = userId
                },
                Deaf = args.Deaf,
                Mute = args.Mute,
                Nickname = args.Nickname,
                Roles = args.Roles
            });
        }
        [HttpDelete("guilds/{guildId}/members/{userId}")]
        public async Task<IActionResult> RemoveGuildMemberAsync(Snowflake guildId, Snowflake userId)
        {
            return NoContent();
        }
        [HttpPatch("guilds/{guildId}/members/{userId}")]
        public async Task<IActionResult> ModifyGuildMemberAsync(Snowflake guildId, Snowflake userId, [FromBody] ModifyGuildMemberParams args)
        {
            args.Validate();

            return NoContent();
        }
        [HttpPatch("guilds/{guildId}/members/@me/nick")]
        public async Task<IActionResult> ModifyCurrentUserNickAsync(Snowflake guildId, [FromBody] ModifyCurrentUserNickParams args)
        {
            args.Validate();

            return NoContent();
        }

        [HttpPut("guilds/{guildId}/members/{userId}/roles/{roleId}")]
        public async Task<IActionResult> AddGuildMemberRoleAsync(Snowflake guildId, Snowflake userId, Snowflake roleId)
        {
            return NoContent();
        }
        [HttpDelete("guilds/{guildId}/members/{userId}/roles/{roleId}")]
        public async Task<IActionResult> RemoveGuildMemberRoleAsync(Snowflake guildId, Snowflake userId, Snowflake roleId)
        {
            return NoContent();
        }

        [HttpGet("guilds/{guildId}/bans")]
        public async Task<IActionResult> GetGuildBansAsync(Snowflake guildId)
        {
            return Ok(new[] { new Ban() });
        }
        [HttpPut("guilds/{guildId}/bans/{userId}")]
        public async Task<IActionResult> CreateGuildBanAsync(Snowflake guildId, Snowflake userId, [FromQuery] Dictionary<string, string> queryMap)
        {
            var args = new CreateGuildBanParams();
            args.LoadQueryMap(queryMap);
            args.Validate();

            return NoContent();
        }
        [HttpDelete("guilds/{guildId}/bans/{userId}")]
        public async Task<IActionResult> DeleteGuildBanAsync(Snowflake guildId, Snowflake userId)
        {
            return NoContent();
        }

        [HttpGet("guilds/{guildId}/roles")]
        public async Task<IActionResult> GetGuildRolesAsync(Snowflake guildId)
        {
            return Ok(new[] { new Role() });
        }
        [HttpPost("guilds/{guildId}/roles")]
        public async Task<IActionResult> CreateGuildRoleAsync(Snowflake guildId, [FromBody] CreateGuildRoleParams args)
        {
            args.Validate();

            return Ok(new Role
            {
                Color = args.Color.GetValueOrDefault(Color.Default),
                IsHoisted = args.IsHoisted.GetValueOrDefault(false),
                IsMentionable = args.IsMentionable.GetValueOrDefault(false),
                Name = args.Name.GetValueOrDefault((Utf8String)"new role"),
                Permissions = args.Permissions.GetValueOrDefault(GuildPermissions.None)
            });
        }
        [HttpDelete("guilds/{guildId}/roles/{roleId}")]
        public async Task<IActionResult> DeleteGuildRoleAsync(Snowflake guildId, Snowflake roleId)
        {
            return Ok(new Role
            {
                Id = roleId
            });
        }
        [HttpPatch("guilds/{guildId}/roles/{roleId}")]
        public async Task<IActionResult> ModifyGuildRoleAsync(Snowflake guildId, Snowflake roleId, [FromBody] ModifyGuildRoleParams args)
        {
            args.Validate();

            var role = new Role
            {
                Id = roleId
            };
            if (args.Color.IsSpecified)
                role.Color = args.Color.Value;
            if (args.IsHoisted.IsSpecified)
                role.IsHoisted = args.IsHoisted.Value;
            if (args.IsMentionable.IsSpecified)
                role.IsMentionable = args.IsMentionable.Value;
            if (args.Name.IsSpecified)
                role.Name = args.Name.Value;
            if (args.Permissions.IsSpecified)
                role.Permissions = args.Permissions.Value;

            return Ok(role);
        }
        [HttpPatch("guilds/{guildId}/roles")]
        public async Task<IActionResult> ModifyGuildRolePositionsAsync(Snowflake guildId, [FromBody] IEnumerable<ModifyGuildRolePositionParams> args)
        {
            foreach (var arg in args)
                arg.Validate();

            return NoContent();
        }

        [HttpGet("guilds/{guildId}/prune")]
        public async Task<IActionResult> GetGuildPruneCountAsync(Snowflake guildId, [FromQuery] Dictionary<string, string> queryMap)
        {
            var args = new GuildPruneParams(0);
            args.LoadQueryMap(queryMap);
            args.Validate();

            return Ok(new GuildPruneCountResponse());
        }

        [HttpPost("guilds/{guildId}/prune")]
        public async Task<IActionResult> PruneGuildMembersAsync(Snowflake guildId, [FromQuery] Dictionary<string, string> queryMap)
        {
            var args = new GuildPruneParams(0);
            args.LoadQueryMap(queryMap);
            args.Validate();

            return Ok(new GuildPruneCountResponse());
        }

        [HttpGet("guilds/{guildId}/regions")]
        public async Task<IActionResult> GetGuildVoiceRegionsAsync(Snowflake guildId)
        {
            return Ok(new[] { new VoiceRegion()});
        }

        [HttpGet("guilds/{guildId}/invites")]
        public async Task<IActionResult> GetGuildInvitesAsync(Snowflake guildId)
        {
            return Ok(new[] { new InviteMetadata() {
                Guild = new InviteGuild
                {
                    Id = guildId
                }
            }});
        }

        [HttpGet("guilds/{guildId}/integrations")]
        public async Task<IActionResult> GetGuildIntegrationsAsync(Snowflake guildId)
        {
            return Ok(new[] { new Integration() });
        }
        [HttpPost("guilds/{guildId}/integrations")]
        public async Task<IActionResult> CreateGuildIntegrationAsync(Snowflake guildId, [FromBody] CreateGuildIntegrationParams args)
        {
            args.Validate();

            return Ok(new Integration
            {
                Id = args.IntegrationId,
                Type = args.Type
            });
        }
        [HttpDelete("guilds/{guildId}/integrations/{integrationId}")]
        public async Task<IActionResult> DeleteGuildIntegrationAsync(Snowflake guildId, Snowflake integrationId)
        {
            return NoContent();
        }
        [HttpPatch("guilds/{guildId}/integrations/{integrationId}")]
        public async Task<IActionResult> ModifyGuildIntegrationAsync(Snowflake guildId, Snowflake integrationId, [FromBody] ModifyGuildIntegrationParams args)
        {
            args.Validate();

            return NoContent();
        }
        [HttpPost("guilds/{guildId}/integrations/{integrationId}/sync")]
        public async Task<IActionResult> SyncGuildIntegrationAsync(Snowflake guildId, Snowflake integrationId)
        {
            return NoContent();
        }

        [HttpGet("guilds/{guildId}/embed")]
        public async Task<IActionResult> GetGuildEmbedAsync(Snowflake guildId)
        {
            return Ok(new GuildEmbed());
        }
        [HttpPatch("guilds/{guildId}/embed")]
        public async Task<IActionResult> ModifyGuildEmbedAsync(Snowflake guildId, [FromBody] ModifyGuildEmbedParams args)
        {
            args.Validate();

            var embed = new GuildEmbed();
            if (args.ChannelId.IsSpecified)
                embed.ChannelId = args.ChannelId.Value;
            if (args.Enabled.IsSpecified)
                embed.Enabled = args.Enabled.Value;

            return Ok(embed);
        }
    }
}
