using System;
using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Etf.Tests
{
    public class GuidTests : BaseTest<Guid>
    {
        public static IEnumerable<object[]> GetDData() => TextToBinary(Utf8.Tests.GuidTests.GetDData());
        public static IEnumerable<object[]> GetBData() => TextToBinary(Utf8.Tests.GuidTests.GetBData());
        public static IEnumerable<object[]> GetPData() => TextToBinary(Utf8.Tests.GuidTests.GetPData());
        public static IEnumerable<object[]> GetNData() => TextToBinary(Utf8.Tests.GuidTests.GetNData());

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(BinaryTestData<Guid> data) => RunTest(data, new GuidEtfConverter('D'));
        [Theory]
        [MemberData(nameof(GetBData))]
        public void Format_B(BinaryTestData<Guid> data) => RunTest(data, new GuidEtfConverter('B'));
        [Theory]
        [MemberData(nameof(GetPData))]
        public void Format_P(BinaryTestData<Guid> data) => RunTest(data, new GuidEtfConverter('P'));
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(BinaryTestData<Guid> data) => RunTest(data, new GuidEtfConverter('N'));
    }
}
