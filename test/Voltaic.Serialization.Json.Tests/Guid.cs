using System;
using System.Collections.Generic;
using Voltaic.Serialization.Utf8.Tests;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class GuidTests : BaseTest<Guid>
    {
        public static IEnumerable<object[]> GetDData() => Utf8.Tests.GuidTests.GetDData();
        public static IEnumerable<object[]> GetBData() => Utf8.Tests.GuidTests.GetBData();
        public static IEnumerable<object[]> GetPData() => Utf8.Tests.GuidTests.GetPData();
        public static IEnumerable<object[]> GetNData() => Utf8.Tests.GuidTests.GetNData();

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<Guid> data) => RunQuoteTest(data);
        [Theory]
        [MemberData(nameof(GetBData))]
        public void Format_B(TextTestData<Guid> data) => RunQuoteTest(data, new GuidJsonConverter('B'));
        [Theory]
        [MemberData(nameof(GetPData))]
        public void Format_P(TextTestData<Guid> data) => RunQuoteTest(data, new GuidJsonConverter('P'));
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<Guid> data) => RunQuoteTest(data, new GuidJsonConverter('N'));
    }
}
