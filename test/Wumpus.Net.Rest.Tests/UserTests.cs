using System.Linq;
using Voltaic;
using Wumpus.Requests;
using Xunit;

namespace Wumpus.Rest.Tests
{
    public class UserTests : BaseTest
    {
        [Fact]
        public void GetCurrentUserAsync()
        {
            RunTest(c => c.GetCurrentUserAsync());
        }
        [Fact]
        public void GetUserAsync()
        {
            RunTest(c => c.GetUserAsync(789), x =>
            {
                Assert.Equal(789UL, x.Id.RawValue);
            });
        }
        [Fact]
        public void ModifyCurrentUserAsync()
        {
            RunTest(c => c.ModifyCurrentUserAsync(new ModifyCurrentUserParams
            {
                // TODO: Impl
            }), x =>
            {
                // TODO: Impl
            });
        }

        [Fact]
        public void GetCurrentUserGuildsAsync()
        {
            RunTest(c => c.GetCurrentUserGuildsAsync(new GetCurrentUserGuildsParams
            {
                // TODO: Impl
            }));
        }
        [Fact]
        public void LeaveGuildAsync()
        {
            RunTest(c => c.LeaveGuildAsync(123));
        }

        [Fact]
        public void GetDMChannelsAsync()
        {
            RunTest(c => c.GetDMChannelsAsync());
        }
        [Fact]
        public void CreateDMChannelAsync()
        {
            RunTest(c => c.CreateDMChannelAsync(new CreateDMChannelParams(789UL)), x =>
            {
                Assert.Contains(789UL, x.Recipients.Value.Select(y => y.Id.RawValue));
            });
        }
        [Fact]
        public void CreateGroupDMChannelAsync()
        {
            RunTest(c => c.CreateGroupDMChannelAsync(new CreateGroupDMChannelParams(new[] { (Utf8String)"access_token1", (Utf8String)"access_token2" })));
        }

        [Fact]
        public void GetUserConnectionsAsync()
        {
            RunTest(c => c.GetUserConnectionsAsync());
        }
    }
}
