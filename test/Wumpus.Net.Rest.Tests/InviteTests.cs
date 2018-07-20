using Voltaic;
using Wumpus.Requests;
using Xunit;

namespace Wumpus.Rest.Tests
{
    public class InviteTests : BaseTest
    {
        [Fact]
        public void GetInviteAsync()
        {
            RunTest(c => c.GetInviteAsync((Utf8String)"invite_code", new GetInviteParams
            {
                WithCounts = true
            }), x =>
            {
                Assert.Equal((Utf8String)"invite_code", x.Code);
            });
        }
        [Fact]
        public void DeleteInviteAsync()
        {
            RunTest(c => c.DeleteInviteAsync((Utf8String)"invite_code"), x =>
            {
                Assert.Equal((Utf8String)"invite_code", x.Code);
            });
        }
    }
}
