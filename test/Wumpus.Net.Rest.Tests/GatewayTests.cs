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
            RunTest(c => c.GetGatewayAsync());
        }

        [Fact]
        public void GetBotGatewayAsync()
        {
            RunTest(c => c.GetBotGatewayAsync());
        }
    }
}
