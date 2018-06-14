using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class ObjectTests : BaseTest<object>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return ReadWrite("null", null);

            // TODO: Impl more
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void TestObject(TestData data) => Test(data);
    }
}
