using Voltaic;
using Wumpus.Responses;
using Xunit;

namespace Wumpus.Rest.Tests
{
    public class GatewayTests : BaseTest
    {
        [Fact]
        public void GetGatewayAsync()
        {
            RunTest(c => c.GetGatewayAsync(), new GetGatewayResponse
            {
                Url = (Utf8String)"http://localhost:34560"
            });
        }

        [Fact]
        public void GetBotGatewayAsync()
        {
            RunTest(c => c.GetBotGatewayAsync(), new GetBotGatewayResponse
            {
                Url = (Utf8String)"http://localhost:34560",
                Shards = 1
            });
        }
    }
}
