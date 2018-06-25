using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class BooleanTests : BaseTest<bool>
    {
        public static IEnumerable<object[]> GetGData() => Utf8.Tests.BooleanTests.GetGData();
        public static IEnumerable<object[]> GetLittleLData() => Utf8.Tests.BooleanTests.GetLittleLData();

        [Theory]
        [MemberData(nameof(GetLittleLData))]
        public void Boolean(TestData<bool> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetLittleLData))]
        public void Format_LittleL(TestData<bool> data) => RunQuoteTest(data, onlyReads: true);
        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_G(TestData<bool> data) => RunQuoteTest(data, new BooleanJsonConverter('G'));

    }
}
