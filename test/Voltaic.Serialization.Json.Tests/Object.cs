using System.Collections.Generic;
using Voltaic.Serialization.Tests;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class ObjectTests : BaseTest<object>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return ReadWrite("null", null);

            // TODO: Impl more
            // TODO: Add Optional<>
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Object(TestData<object> data) => RunTest(data);
    }
}
