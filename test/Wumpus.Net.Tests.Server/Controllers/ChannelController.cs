#pragma warning disable CS1998

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Voltaic;
using Wumpus.Entities;
using Wumpus.Requests;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class Controller : ControllerBase
    {
        [HttpGet("channels/{channelId}")]
        public async Task<IActionResult> GetChannelAsync(Snowflake channelId)
        {
            return Ok(new Channel
            {
                Id = channelId
            });
        }
        [HttpPut("channels/{channelId}")]
        public async Task<IActionResult> ReplaceTextChannelAsync(Snowflake channelId, [FromBody] ModifyTextChannelParams args)
        {
            args.Validate();
            var channel = new Channel
            {
                Id = channelId,
                Type = ChannelType.Text,
                Name = args.Name,
                ParentId = args.ParentId,
                PermissionOverwrites = args.PermissionOverwrites,
                Position = args.Position,
                Topic = args.Topic
            };
            return Ok(channel);
        }
        [HttpPut("channels/{channelId}")]
        public async Task<IActionResult> ReplaceVoiceChannelAsync(Snowflake channelId, [FromBody] ModifyVoiceChannelParams args)
        {
            args.Validate();
            var channel = new Channel
            {
                Id = channelId,
                Type = ChannelType.Voice,
                Bitrate = args.Bitrate,
                Name = args.Name,
                ParentId = args.ParentId,
                PermissionOverwrites = args.PermissionOverwrites,
                Position = args.Position,
                UserLimit = args.UserLimit
            };
            return Ok(channel);
        }
        [HttpPatch("channels/{channelId}")]
        public async Task<IActionResult> ModifyGuildChannelAsync(Snowflake channelId, [FromBody] ModifyGuildChannelParams args)
        {
            var channel = new Channel
            {
                Id = channelId,
                Type = ChannelType.Text,
                Name = args.Name,
                PermissionOverwrites = args.PermissionOverwrites,
                Position = args.Position
            };
            return Ok(channel);
        }
        [HttpPatch("channels/{channelId}")]
        public async Task<IActionResult> ModifyTextChannelAsync(Snowflake channelId, [FromBody] ModifyTextChannelParams args)
        {
            var channel = new Channel
            {
                Id = channelId,
                Type = ChannelType.Text,
                Name = args.Name,
                ParentId = args.ParentId,
                PermissionOverwrites = args.PermissionOverwrites,
                Position = args.Position,
                Topic = args.Topic
            };
            return Ok(channel);
        }
        [HttpPatch("channels/{channelId}")]
        public async Task<IActionResult> ModifyVoiceChannelAsync(Snowflake channelId, [FromBody] ModifyVoiceChannelParams args)
        {
            var channel = new Channel
            {
                Id = channelId,
                Type = ChannelType.Voice,
                Bitrate = args.Bitrate,
                Name = args.Name,
                ParentId = args.ParentId,
                PermissionOverwrites = args.PermissionOverwrites,
                Position = args.Position,
                UserLimit = args.UserLimit
            };
            return Ok(channel);
        }
        [HttpDelete("channels/{channelId}")]
        public async Task<IActionResult> DeleteChannelAsync(Snowflake channelId)
        {
            return Ok(new Channel
            {
                Id = channelId
            });
        }

        [HttpGet("channels/{channelId}/messages")]
        public async Task<IActionResult> GetChannelMessagesAsync(Snowflake channelId, GetChannelMessagesParams args)
        {
            return BadRequest();
        }
        [HttpGet("channels/{channelId}/messages/{messageId}")]
        public async Task<IActionResult> GetChannelMessageAsync(Snowflake channelId, Snowflake messageId)
        {
            return BadRequest();
        }
        [HttpPost("channels/{channelId}/messages")]
        public async Task<IActionResult> CreateMessageAsync(Snowflake channelId, [FromBody] CreateMessageParams args)
        {
            return BadRequest();
        }
        [HttpPatch("channels/{channelId}/messages/{messageId}")]
        public async Task<IActionResult> ModifyMessageAsync(Snowflake channelId, Snowflake messageId, [FromBody] ModifyMessageParams args)
        {
            return BadRequest();
        }
        [HttpDelete("channels/{channelId}/messages/{messageId}")]
        public async Task<IActionResult> DeleteMessageAsync(Snowflake channelId, Snowflake messageId)
        {
            return NoContent();
        }
        [HttpPost("channels/{channelId}/messages/bulk-delete")]
        public async Task<IActionResult> DeleteMessagesAsync(Snowflake channelId, [FromBody] DeleteMessagesParams args)
        {
            return NoContent();
        }

        [HttpGet("channels/{channelId}/messages/{messageId}/reactions/{emoji}")]
        public async Task<IActionResult> GetReactionUsersAsync(Snowflake channelId, Snowflake messageId, Utf8String emoji)
        {
            return BadRequest();
        }
        [HttpPut("channels/{channelId}/messages/{messageId}/reactions/{emoji}/@me")]
        public async Task<IActionResult> CreateReactionAsync(Snowflake channelId, Snowflake messageId, Utf8String emoji)
        {
            return NoContent();
        }
        [HttpDelete("channels/{channelId}/messages/{messageId}/reactions/{emoji}/@me")]
        public async Task<IActionResult> DeleteReactionAsync(Snowflake channelId, Snowflake messageId, Utf8String emoji)
        {
            return NoContent();
        }
        [HttpDelete("channels/{channelId}/messages/{messageId}/reactions/{emoji}/{userId}")]
        public async Task<IActionResult> DeleteReactionAsync(Snowflake channelId, Snowflake messageId, Snowflake userId, Utf8String emoji)
        {
            return NoContent();
        }
        [HttpDelete("channels/{channelId}/messages/{messageId}/reactions")]
        public async Task<IActionResult> DeleteAllReactionsAsync(Snowflake channelId, Snowflake messageId)
        {
            return NoContent();
        }

        [HttpPut("channels/{channelId}/permissions/{overwriteId}")]
        public async Task<IActionResult> EditChannelPermissionsAsync(Snowflake channelId, Snowflake overwriteId, [FromBody] ModifyChannelPermissionsParams args)
        {
            return NoContent();
        }
        [HttpDelete("channels/{channelId}/permissions/{overwriteId}")]
        public async Task<IActionResult> DeleteChannelPermissionsAsync(Snowflake channelId, Snowflake overwriteId)
        {
            return NoContent();
        }

        [HttpGet("channels/{channelId}/invites")]
        public async Task<IActionResult> GetChannelInvitesAsync(Snowflake channelId)
        {
            return BadRequest();
        }
        [HttpPost("channels/{channelId}/invites")]
        public async Task<IActionResult> CreateChannelInviteAsync(Snowflake channelId, [FromBody] CreateChannelInviteParams args)
        {
            return BadRequest();
        }

        [HttpGet("channels/{channelId}/pins")]
        public async Task<IActionResult> GetPinnedMessagesAsync(Snowflake channelId)
        {
            return BadRequest();
        }
        [HttpPut("channels/{channelId}/pins/{messageId}")]
        public async Task<IActionResult> PinMessageAsync(Snowflake channelId, Snowflake messageId)
        {
            return NoContent();
        }
        [HttpDelete("channels/{channelId}/pins/{messageId}")]
        public async Task<IActionResult> UnpinMessageAsync(Snowflake channelId, Snowflake messageId)
        {
            return NoContent();
        }

        [HttpPut("channels/{channelId}/recipients/{userId}")]
        public async Task<IActionResult> AddRecipientAsync(Snowflake channelId, Snowflake userId, [FromBody] AddChannelRecipientParams args)
        {
            return NoContent();
        }
        [HttpDelete("channels/{channelId}/recipients/{userId}")]
        public async Task<IActionResult> RemoveRecipientAsync(Snowflake channelId, Snowflake userId)
        {
            return NoContent();
        }

        [HttpPost("channels/{channelId}/typing")]
        public async Task<IActionResult> TriggerTypingIndicatorAsync(Snowflake channelId)
        {
            return BadRequest();
        }
    }
}
