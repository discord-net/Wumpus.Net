using Xunit;

namespace Wumpus.Rest.Tests
{
    public class OAuthTests : BaseTest
    {
        [Fact]
        public void GetCurrentApplicationAsync()
        {
            RunTest(c => c.GetCurrentApplicationAsync());
        }
    }
}
