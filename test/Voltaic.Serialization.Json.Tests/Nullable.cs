using System.Collections.Generic;
using Voltaic.Serialization.Utf8.Tests;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class NullableTests : BaseTest<int?>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return ReadWrite("null", null);
            yield return FailRead("\"null\"");
            yield return FailRead("");
            yield return ReadWrite("1", 1);
            yield return Read("\"1\"", 1);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TextTestData<int?> data) => RunTest(data);
    }
}
