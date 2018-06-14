using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class BooleanTests : BaseTest<bool>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return Fail("null");
            yield return ReadWrite("false", false);
            yield return Read("False", false);
            yield return ReadWrite("true", true);
            yield return Read("True", true);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void TestBoolean(TestData data) => Test(data, testQuoted: true);
    }
}
