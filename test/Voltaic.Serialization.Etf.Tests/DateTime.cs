using System;
using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Etf.Tests
{
    public class DateTimeTests : BaseTest<DateTime>
    {
        //public static IEnumerable<object[]> GetDefaultData() => TextToBinary(Utf8.Tests.DateTimeTests.GetDefaultData());
        public static IEnumerable<object[]> GetGData() => TextToBinary(Utf8.Tests.DateTimeTests.GetGData());
        public static IEnumerable<object[]> GetRData() => TextToBinary(Utf8.Tests.DateTimeTests.GetRData());
        public static IEnumerable<object[]> GetLittleLData() => TextToBinary(Utf8.Tests.DateTimeTests.GetLittleLData());
        public static IEnumerable<object[]> GetOData() => TextToBinary(Utf8.Tests.DateTimeTests.GetOData());

        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_G(BinaryTestData<DateTime> data) => RunTest(data, new DateTimeEtfConverter('G'));
        //[Theory]
        //[MemberData(nameof(GetDefaultData))]
        //public void Format_Default(BinaryTestData<DateTime> data) => RunQuoteTest(data, new DateTimeEtfConverter(default));
        [Theory]
        [MemberData(nameof(GetRData))]
        public void Format_R(BinaryTestData<DateTime> data) => RunTest(data, new DateTimeEtfConverter('R'));
        [Theory]
        [MemberData(nameof(GetLittleLData))]
        public void Format_LittleL(BinaryTestData<DateTime> data) => RunTest(data, new DateTimeEtfConverter('l'));
        [Theory]
        [MemberData(nameof(GetOData))]
        public void Format_O(BinaryTestData<DateTime> data) => RunTest(data, new DateTimeEtfConverter('O'));
    }

    public class DateTimeOffsetTests : BaseTest<DateTimeOffset>
    {
        public static IEnumerable<object[]> GetGData() => TextToBinary(Utf8.Tests.DateTimeOffsetTests.GetDefaultData());
        public static IEnumerable<object[]> GetRData() => TextToBinary(Utf8.Tests.DateTimeOffsetTests.GetRData());
        public static IEnumerable<object[]> GetLittleLData() => TextToBinary(Utf8.Tests.DateTimeOffsetTests.GetLittleLData());
        public static IEnumerable<object[]> GetOData() => TextToBinary(Utf8.Tests.DateTimeOffsetTests.GetOData());

        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_Default(BinaryTestData<DateTimeOffset> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetRData))]
        public void Format_R(BinaryTestData<DateTimeOffset> data) => RunTest(data, new DateTimeOffsetEtfConverter('R'));
        [Theory]
        [MemberData(nameof(GetLittleLData))]
        public void Format_LittleL(BinaryTestData<DateTimeOffset> data) => RunTest(data, new DateTimeOffsetEtfConverter('l'));
        [Theory]
        [MemberData(nameof(GetOData))]
        public void Format_O(BinaryTestData<DateTimeOffset> data) => RunTest(data, new DateTimeOffsetEtfConverter('O'));
    }

    public class TimeSpanTests : BaseTest<TimeSpan>
    {
        public static IEnumerable<object[]> GetLittleCData() => TextToBinary(Utf8.Tests.TimeSpanTests.GetLittleCData());
        public static IEnumerable<object[]> GetGData() => TextToBinary(Utf8.Tests.TimeSpanTests.GetGData());
        public static IEnumerable<object[]> GetLittleGData() => TextToBinary(Utf8.Tests.TimeSpanTests.GetLittleGData());

        [Theory]
        [MemberData(nameof(GetLittleCData))]
        public void Format_LittleC(BinaryTestData<TimeSpan> data) => RunTest(data, new TimeSpanEtfConverter('c'));
        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_G(BinaryTestData<TimeSpan> data) => RunTest(data, new TimeSpanEtfConverter('G'));
        [Theory]
        [MemberData(nameof(GetLittleGData))]
        public void Format_LittleG(BinaryTestData<TimeSpan> data) => RunTest(data, new TimeSpanEtfConverter('g'));
    }
}
