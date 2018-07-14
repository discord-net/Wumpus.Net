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
        // Webhook

        [HttpGet("webhooks/{webhookId}")]
        public async Task<IActionResult> GetWebhookAsync(Snowflake webhookId)
        {
            return BadRequest();
        }
        [HttpDelete("webhooks/{webhookId}")]
        public async Task<IActionResult> DeleteWebhookAsync(Snowflake webhookId)
        {
            return BadRequest();
        }
        [HttpPatch("webhooks/{webhookId}")]
        public async Task<IActionResult> ModifyWebhookAsync(Snowflake webhookId, ModifyWebhookParams args)
        {
            return BadRequest();
        }


        // Webhook (Anonymous)

        [HttpGet("webhooks/{webhookId}/{webhookToken}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWebhookWithTokenAsync(Snowflake webhookId, Utf8String webhookToken)
        {
            return BadRequest();
        }
        [HttpDelete("webhooks/{webhookId}/{webhookToken}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteWebhookAsync(Snowflake webhookId, Utf8String webhookToken)
        {
            return BadRequest();
        }
        [HttpPatch("webhooks/{webhookId}/{webhookToken}")]
        [AllowAnonymous]
        public async Task<IActionResult> ModifyWebhookAsync(Snowflake webhookId, Utf8String webhookToken, ModifyWebhookParams args)
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
