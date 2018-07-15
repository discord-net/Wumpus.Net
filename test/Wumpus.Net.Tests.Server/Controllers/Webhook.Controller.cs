#pragma warning disable CS1998

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voltaic;
using Wumpus.Requests;

namespace Wumpus.Server.Controllers
{
    [ApiController]
    public class WebhookController : ControllerBase
    {
        [HttpGet("channels/{channelId}/webhooks")]
        public async Task<IActionResult> GetChannelWebhooksAsync(Snowflake channelId)
        {
            return BadRequest();
        }
        [HttpGet("guilds/{guildId}/webhooks")]
        public async Task<IActionResult> GetGuildWebhooksAsync(Snowflake guildId)
        {
            return BadRequest();
        }

        [HttpGet("webhooks/{webhookId}")]
        public async Task<IActionResult> GetWebhookAsync(Snowflake webhookId)
        {
            return BadRequest();
        }
        [HttpGet("webhooks/{webhookId}/{webhookToken}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWebhookAsync(Snowflake webhookId, Utf8String webhookToken)
        {
            return BadRequest();
        }

        [HttpPost("channels/{channelId}/webhooks")]
        public async Task<IActionResult> CreateWebhookAsync(Snowflake channelId, [FromBody] CreateWebhookParams args)
        {
            return BadRequest();
        }

        [HttpPatch("webhooks/{webhookId}")]
        public async Task<IActionResult> ModifyWebhookAsync(Snowflake webhookId, ModifyWebhookParams args)
        {
            return BadRequest();
        }
        [HttpPatch("webhooks/{webhookId}/{webhookToken}")]
        [AllowAnonymous]
        public async Task<IActionResult> ModifyWebhookAsync(Snowflake webhookId, Utf8String webhookToken, ModifyWebhookParams args)
        {
            return BadRequest();
        }

        [HttpDelete("webhooks/{webhookId}")]
        public async Task<IActionResult> DeleteWebhookAsync(Snowflake webhookId)
        {
            return BadRequest();
        }
        [HttpDelete("webhooks/{webhookId}/{webhookToken}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteWebhookAsync(Snowflake webhookId, Utf8String webhookToken)
        {
            return BadRequest();
        }

        [HttpPost("webhooks/{webhookId}/{webhookToken}")]
        [AllowAnonymous]
        public async Task<IActionResult> ExecuteWebhookAsync(Snowflake webhookId, Utf8String webhookToken, [FromBody] ExecuteWebhookParams args)
        {
            return BadRequest();
        }
    }
}
