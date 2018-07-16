#pragma warning disable CS1998

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Voltaic;
using Wumpus.Entities;
using Wumpus.Requests;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class ChannelController : ControllerBase
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

            return Ok(new Channel
            {
                Id = channelId,
                Type = ChannelType.Text,
                Name = args.Name,
                ParentId = args.ParentId,
                PermissionOverwrites = args.PermissionOverwrites,
                Position = args.Position,
                Topic = args.Topic
            });
        }
        [HttpPut("channels/{channelId}")]
        public async Task<IActionResult> ReplaceVoiceChannelAsync(Snowflake channelId, [FromBody] ModifyVoiceChannelParams args)
        {
            args.Validate();

            return Ok(new Channel
            {
                Id = channelId,
                Type = ChannelType.Voice,
                Bitrate = args.Bitrate,
                Name = args.Name,
                ParentId = args.ParentId,
                PermissionOverwrites = args.PermissionOverwrites,
                Position = args.Position,
                UserLimit = args.UserLimit
            });
        }
        [HttpPatch("channels/{channelId}")]
        public async Task<IActionResult> ModifyGuildChannelAsync(Snowflake channelId, [FromBody] ModifyGuildChannelParams args)
        {
            args.Validate();

            return Ok(new Channel
            {
                Id = channelId,
                Type = ChannelType.Text,
                Name = args.Name,
                PermissionOverwrites = args.PermissionOverwrites,
                Position = args.Position
            });
        }
        [HttpPatch("channels/{channelId}")]
        public async Task<IActionResult> ModifyTextChannelAsync(Snowflake channelId, [FromBody] ModifyTextChannelParams args)
        {
            args.Validate();

            return Ok(new Channel
            {
                Id = channelId,
                Type = ChannelType.Text,
                Name = args.Name,
                ParentId = args.ParentId,
                PermissionOverwrites = args.PermissionOverwrites,
                Position = args.Position,
                Topic = args.Topic
            });
        }
        [HttpPatch("channels/{channelId}")]
        public async Task<IActionResult> ModifyVoiceChannelAsync(Snowflake channelId, [FromBody] ModifyVoiceChannelParams args)
        {
            args.Validate();

            return Ok(new Channel
            {
                Id = channelId,
                Type = ChannelType.Voice,
                Bitrate = args.Bitrate,
                Name = args.Name,
                ParentId = args.ParentId,
                PermissionOverwrites = args.PermissionOverwrites,
                Position = args.Position,
                UserLimit = args.UserLimit
            });
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
            args.Validate();

            var msg = new Message();
            if (args.Before.IsSpecified)
                msg.Id = args.Before.Value;
            if (args.Around.IsSpecified)
                msg.Id = args.Around.Value;
            if (args.After.IsSpecified)
                msg.Id = args.After.Value;

            return Ok(new[] { msg });
        }
        [HttpGet("channels/{channelId}/messages/{messageId}")]
        public async Task<IActionResult> GetChannelMessageAsync(Snowflake channelId, Snowflake messageId)
        {
            return Ok(new Message
            {
                ChannelId = channelId,
                Id = messageId
            });
        }
        [HttpPost("channels/{channelId}/messages")]
        public async Task<IActionResult> CreateMessageAsync(Snowflake channelId, [FromBody] CreateMessageParams args)
        {
            args.Validate();

            var msg = new Message
            {
                Content = args.Content.GetValueOrDefault((Utf8String)null),
                IsTextToSpeech = args.IsTextToSpeech.GetValueOrDefault(false),
                Nonce = args.Nonce
            };            
            if (args.Embed.IsSpecified)
                msg.Embeds = new[] { args.Embed.Value };
            if (args.File.IsSpecified)
                msg.Attachments = new[] { new Attachment { Filename = args.File.Value.Filename } };

            return Ok(msg);
        }
        [HttpPatch("channels/{channelId}/messages/{messageId}")]
        public async Task<IActionResult> ModifyMessageAsync(Snowflake channelId, Snowflake messageId, [FromBody] ModifyMessageParams args)
        {
            args.Validate();

            var msg = new Message
            {
                Id = messageId,
                ChannelId = channelId
            };
            if (args.Content.IsSpecified)
                args.Content = args.Content.Value;
            if (args.Embed.IsSpecified)
                msg.Embeds = new[] { args.Embed.Value };

            return Ok(msg);
        }
        [HttpDelete("channels/{channelId}/messages/{messageId}")]
        public async Task<IActionResult> DeleteMessageAsync(Snowflake channelId, Snowflake messageId)
        {
            return NoContent();
        }
        [HttpPost("channels/{channelId}/messages/bulk-delete")]
        public async Task<IActionResult> DeleteMessagesAsync(Snowflake channelId, [FromBody] DeleteMessagesParams args)
        {
            args.Validate();

            return NoContent();
        }

        [HttpGet("channels/{channelId}/messages/{messageId}/reactions/{emoji}")]
        public async Task<IActionResult> GetReactionUsersAsync(Snowflake channelId, Snowflake messageId, Utf8String emoji)
        {
            return Ok(new[] { new User() });
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
            args.Validate();

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
            return Ok(new[] { new Invite
            {
                Channel = new InviteChannel
                {
                    Id = channelId
                }
            }});
        }
        [HttpPost("channels/{channelId}/invites")]
        public async Task<IActionResult> CreateChannelInviteAsync(Snowflake channelId, [FromBody] CreateChannelInviteParams args)
        {
            args.Validate();

            return Ok(new Invite
            {
                Channel = new InviteChannel
                {
                    Id = channelId
                }
            });
        }

        [HttpGet("channels/{channelId}/pins")]
        public async Task<IActionResult> GetPinnedMessagesAsync(Snowflake channelId)
        {
            return Ok(new Message
            {
                ChannelId = channelId
            });
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
            args.Validate();

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
            return NoContent();
        }
    }
}
