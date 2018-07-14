#pragma warning disable CS1998

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Voltaic;
using Wumpus.Requests;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class ChannelController : ControllerBase
    {
        // Channel

        [HttpGet("channels/{channelId}")]
        public async Task<IActionResult> GetChannelAsync(Snowflake channelId)
        {
            return BadRequest();
        }
        [HttpPut("channels/{channelId}")]
        public async Task<IActionResult> ReplaceTextChannelAsync(Snowflake channelId, [FromBody] ModifyTextChannelParams args)
        {
            return BadRequest();
        }
        [HttpPut("channels/{channelId}")]
        public async Task<IActionResult> ReplaceVoiceChannelAsync(Snowflake channelId, [FromBody] ModifyVoiceChannelParams args)
        {
            return BadRequest();
        }
        [HttpPatch("channels/{channelId}")]
        public async Task<IActionResult> ModifyGuildChannelAsync(Snowflake channelId, [FromBody] ModifyGuildChannelParams args)
        {
            return BadRequest();
        }
        [HttpPatch("channels/{channelId}")]
        public async Task<IActionResult> ModifyTextChannelAsync(Snowflake channelId, [FromBody] ModifyTextChannelParams args)
        {
            return BadRequest();
        }
        [HttpPatch("channels/{channelId}")]
        public async Task<IActionResult> ModifyVoiceChannelAsync(Snowflake channelId, [FromBody] ModifyVoiceChannelParams args)
        {
            return BadRequest();
        }
        [HttpDelete("channels/{channelId}")]
        public async Task<IActionResult> DeleteChannelAsync(Snowflake channelId)
        {
            return BadRequest();
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
            return BadRequest();
        }
        [HttpPost("channels/{channelId}/messages/bulk-delete")]
        public async Task<IActionResult> DeleteMessagesAsync(Snowflake channelId, [FromBody] DeleteMessagesParams args)
        {
            return BadRequest();
        }

        [HttpGet("channels/{channelId}/messages/{messageId}/reactions/{emoji}")]
        public async Task<IActionResult> GetReactionUsersAsync(Snowflake channelId, Snowflake messageId, Utf8String emoji)
        {
            return BadRequest();
        }
        [HttpPut("channels/{channelId}/messages/{messageId}/reactions/{emoji}/@me")]
        public async Task<IActionResult> CreateReactionAsync(Snowflake channelId, Snowflake messageId, Utf8String emoji)
        {
            return BadRequest();
        }
        [HttpDelete("channels/{channelId}/messages/{messageId}/reactions/{emoji}/@me")]
        public async Task<IActionResult> DeleteReactionAsync(Snowflake channelId, Snowflake messageId, Utf8String emoji)
        {
            return BadRequest();
        }
        [HttpDelete("channels/{channelId}/messages/{messageId}/reactions/{emoji}/{userId}")]
        public async Task<IActionResult> DeleteReactionAsync(Snowflake channelId, Snowflake messageId, Snowflake userId, Utf8String emoji)
        {
            return BadRequest();
        }
        [HttpDelete("channels/{channelId}/messages/{messageId}/reactions")]
        public async Task<IActionResult> DeleteAllReactionsAsync(Snowflake channelId, Snowflake messageId)
        {
            return BadRequest();
        }

        [HttpPut("channels/{channelId}/permissions/{overwriteId}")]
        public async Task<IActionResult> EditChannelPermissionsAsync(Snowflake channelId, Snowflake overwriteId, [FromBody] ModifyChannelPermissionsParams args)
        {
            return BadRequest();
        }
        [HttpDelete("channels/{channelId}/permissions/{overwriteId}")]
        public async Task<IActionResult> DeleteChannelPermissionsAsync(Snowflake channelId, Snowflake overwriteId)
        {
            return BadRequest();
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
            return BadRequest();
        }
        [HttpDelete("channels/{channelId}/pins/{messageId}")]
        public async Task<IActionResult> UnpinMessageAsync(Snowflake channelId, Snowflake messageId)
        {
            return BadRequest();
        }

        [HttpPut("channels/{channelId}/recipients/{userId}")]
        public async Task<IActionResult> AddRecipientAsync(Snowflake channelId, Snowflake userId, [FromBody] AddChannelRecipientParams args)
        {
            return BadRequest();
        }
        [HttpDelete("channels/{channelId}/recipients/{userId}")]
        public async Task<IActionResult> RemoveRecipientAsync(Snowflake channelId, Snowflake userId)
        {
            return BadRequest();
        }

        [HttpPost("channels/{channelId}/webhooks")]
        public async Task<IActionResult> CreateWebhookAsync(Snowflake channelId, [FromBody] CreateWebhookParams args)
        {
            return BadRequest();
        }
        [HttpGet("channels/{channelId}/webhooks")]
        public async Task<IActionResult> GetChannelWebhooksAsync(Snowflake channelId)
        {
            return BadRequest();
        }

        [HttpPost("channels/{channelId}/typing")]
        public async Task<IActionResult> TriggerTypingIndicatorAsync(Snowflake channelId)
        {
            return BadRequest();
        }
    }
}
