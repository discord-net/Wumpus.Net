using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class DictionaryTests : BaseTest<Dictionary<string, string>>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return ReadWrite("null", null);

            // TODO: Impl more
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TestData data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetData))]
        public void TestWhitespace(TestData data) => RunWhitespaceTest(data);
    }
}
