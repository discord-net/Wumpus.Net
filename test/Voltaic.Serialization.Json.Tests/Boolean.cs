using System.Collections.Generic;
using Voltaic.Serialization.Utf8.Tests;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class BooleanTests : BaseTest<bool>
    {
        public static IEnumerable<object[]> GetGData() => Utf8.Tests.BooleanTests.GetGData();
        public static IEnumerable<object[]> GetLittleLData() => Utf8.Tests.BooleanTests.GetLittleLData();

        [Theory]
        [MemberData(nameof(GetLittleLData))]
        public void Boolean(TextTestData<bool> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetLittleLData))]
        public void Format_LittleL(TextTestData<bool> data) => RunQuoteTest(data, onlyReads: true);
        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_G(TextTestData<bool> data) => RunQuoteTest(data, new BooleanJsonConverter('G'));

    }
}
