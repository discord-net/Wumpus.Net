#pragma warning disable CS1998

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voltaic;
using Wumpus.Entities;
using Wumpus.Requests;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class WebhookController : ControllerBase
    {
        [HttpGet("channels/{channelId}/webhooks")]
        public async Task<IActionResult> GetChannelWebhooksAsync(Snowflake channelId)
        {
            return Ok(new[] { new Webhook
            {
                ChannelId = channelId
            }});
        }
        [HttpGet("guilds/{guildId}/webhooks")]
        public async Task<IActionResult> GetGuildWebhooksAsync(Snowflake guildId)
        {
            return Ok(new[] { new Webhook
            {
                GuildId = guildId
            }});
        }

        [HttpGet("webhooks/{webhookId}")]
        public async Task<IActionResult> GetWebhookAsync(Snowflake webhookId)
        {
            return Ok(new[] { new Webhook
            {
                Id = webhookId
            }});
        }
        [HttpGet("webhooks/{webhookId}/{webhookToken}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWebhookAsync(Snowflake webhookId, Utf8String webhookToken)
        {
            return Ok(new[] { new Webhook
            {
                Id = webhookId,
                Token = webhookToken
            }});
        }

        [HttpPost("channels/{channelId}/webhooks")]
        public async Task<IActionResult> CreateWebhookAsync(Snowflake channelId, [FromBody] CreateWebhookParams args)
        {
            args.Validate();

            return Ok(new[] { new Webhook
            {
                ChannelId = channelId,
                Avatar = args.Avatar,
                Name = args.Name
            }});
        }

        [HttpDelete("webhooks/{webhookId}")]
        public async Task<IActionResult> DeleteWebhookAsync(Snowflake webhookId)
        {
            return NoContent();
        }
        [HttpDelete("webhooks/{webhookId}/{webhookToken}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteWebhookAsync(Snowflake webhookId, Utf8String webhookToken)
        {
            return NoContent();
        }

        [HttpPatch("webhooks/{webhookId}")]
        public async Task<IActionResult> ModifyWebhookAsync(Snowflake webhookId, ModifyWebhookParams args)
        {
            args.Validate();

            var webhook = new Webhook
            {
                Id = webhookId
            };
            if (args.Avatar.IsSpecified)
                webhook.Avatar = args.Avatar.Value;
            if (args.ChannelId.IsSpecified)
                webhook.ChannelId = args.ChannelId.Value;
            if (args.Name.IsSpecified)
                webhook.Name = args.Name.Value;

            return Ok(webhook);
        }
        [HttpPatch("webhooks/{webhookId}/{webhookToken}")]
        [AllowAnonymous]
        public async Task<IActionResult> ModifyWebhookAsync(Snowflake webhookId, Utf8String webhookToken, ModifyWebhookParams args)
        {
            args.Validate();

            var webhook = new Webhook
            {
                Id = webhookId,
                Token = webhookToken
            };
            if (args.Avatar.IsSpecified)
                webhook.Avatar = args.Avatar.Value;
            if (args.ChannelId.IsSpecified)
                webhook.ChannelId = args.ChannelId.Value;
            if (args.Name.IsSpecified)
                webhook.Name = args.Name.Value;

            return Ok(webhook);
        }

        [HttpPost("webhooks/{webhookId}/{webhookToken}")]
        [AllowAnonymous]
        public async Task<IActionResult> ExecuteWebhookAsync(Snowflake webhookId, Utf8String webhookToken, [FromBody] ExecuteWebhookParams args)
        {
            args.Validate();

            return NoContent();
        }
    }
}
