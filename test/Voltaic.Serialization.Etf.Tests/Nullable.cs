using System;
using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Etf.Tests
{
    public class NullableTests : BaseTest<int?>
    {
        public static IEnumerable<object[]> GetData()
        {
            throw new NotImplementedException();
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(BinaryTestData<int?> data) => RunTest(data);
    }
}
