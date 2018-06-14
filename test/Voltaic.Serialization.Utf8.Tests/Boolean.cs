using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Utf8.Tests
{
    public class BooleanTests : BaseTest<bool>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return Fail("null");
            yield return Read("false", false);
            yield return ReadWrite("False", false);
            yield return Read("true", true);
            yield return ReadWrite("True", true);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void TestBoolean(TestData data) => Test(data);
    }
}
