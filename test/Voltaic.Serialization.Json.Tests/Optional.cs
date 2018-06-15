using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class OptionalTests : BaseTest<Optional<string>>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return ReadWrite("", ""); // TODO: Impl
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TestData data) => RunTest(data);

        [Theory]
        [MemberData(nameof(GetData))]
        public void TestWhitespace(TestData data) => RunWhitespaceTest(data);
    }
}
