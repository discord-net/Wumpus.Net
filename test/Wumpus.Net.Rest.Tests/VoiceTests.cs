using Xunit;

namespace Wumpus.Rest.Tests
{
    public class VoiceTests : BaseTest
    {
        [Fact]
        public void GetVoiceRegionsAsync()
        {
            RunTest(c => c.GetVoiceRegionsAsync());
        }
    }
}
