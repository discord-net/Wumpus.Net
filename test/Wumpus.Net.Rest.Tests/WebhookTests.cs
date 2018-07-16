using Voltaic;
using Wumpus.Requests;
using Xunit;

namespace Wumpus.Rest.Tests
{
    public class WebhookTests : BaseTest
    {
        [Fact]
        public void GetChannelWebhooksAsync()
        {
            RunTest(c => c.GetChannelWebhooksAsync(123), x =>
            {
                foreach (var webhook in x)
                    Assert.Equal(123UL, webhook.ChannelId.RawValue);
            });
        }
        [Fact]
        public void GetGuildWebhooksAsync()
        {
            RunTest(c => c.GetGuildWebhooksAsync(123), x =>
            {
                foreach (var webhook in x)
                    Assert.Equal(123UL, webhook.GuildId.Value.RawValue);
            });
        }

        [Fact]
        public void GetWebhookAsync()
        {
            RunTest(c => c.GetWebhookAsync(123), x =>
            {
                Assert.Equal(123UL, x.Id.RawValue);
            });
            RunTest(c => c.GetWebhookAsync(123, (Utf8String)"webhook_token"), x =>
            {
                Assert.Equal(123UL, x.Id.RawValue);
                Assert.Equal((Utf8String)"webhook_token", x.Token);
            });
        }

        [Fact]
        public void CreateWebhookAsync()
        {
            RunTest(c => c.CreateWebhookAsync(123, new CreateWebhookParams
            {
                // TODO: Impl
            }), x =>
            {
                // TODO: Impl
            });
        }

        [Fact]
        public void DeleteWebhookAsync()
        {
            RunTest(c => c.DeleteWebhookAsync(123));
            RunTest(c => c.DeleteWebhookAsync(123, (Utf8String)"webhook_token"));
        }

        [Fact]
        public void ModifyWebhookAsync()
        {
            RunTest(c => c.ModifyWebhookAsync(123, new ModifyWebhookParams
            {
                // TODO: Impl
            }), x =>
            {
                // TODO: Impl
            });
            RunTest(c => c.ModifyWebhookAsync(123, (Utf8String)"webhook_token", new ModifyWebhookParams
            {
                // TODO: Impl
            }), x =>
            {
                // TODO: Impl
            });
        }

        [Fact]
        public void ExecuteWebhookAsync()
        {
            RunTest(c => c.ExecuteWebhookAsync(123, (Utf8String)"webhook_token", new ExecuteWebhookParams
            {
                // TODO: Impl
            }));
        }
    }
}
