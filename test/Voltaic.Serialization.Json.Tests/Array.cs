using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class ArrayTests : BaseTest<string[]>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return ReadWrite("null", null);

            // TODO: Impl more
        } 

        [Theory]
        [MemberData(nameof(GetData))]
        public void TestArray(TestData data) => Test(data);
    }
}
