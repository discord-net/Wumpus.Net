using System;
using System.Collections.Generic;
using Voltaic.Serialization.Utf8.Tests;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class DateTimeTests : BaseTest<DateTime>
    {
        //public static IEnumerable<object[]> GetDefaultData() => Utf8.Tests.DateTimeTests.GetDefaultData();
        public static IEnumerable<object[]> GetGData() => Utf8.Tests.DateTimeTests.GetGData();
        public static IEnumerable<object[]> GetRData() => Utf8.Tests.DateTimeTests.GetRData();
        public static IEnumerable<object[]> GetLittleLData() => Utf8.Tests.DateTimeTests.GetLittleLData();
        public static IEnumerable<object[]> GetOData() => Utf8.Tests.DateTimeTests.GetOData();

        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_G(TextTestData<DateTime> data) => RunQuoteTest(data, new DateTimeJsonConverter('G'));
        //[Theory]
        //[MemberData(nameof(GetDefaultData))]
        //public void Format_Default(TestData<DateTime> data) => RunQuoteTest(data, new DateTimeJsonConverter(default));
        [Theory]
        [MemberData(nameof(GetRData))]
        public void Format_R(TextTestData<DateTime> data) => RunQuoteTest(data, new DateTimeJsonConverter('R'));
        [Theory]
        [MemberData(nameof(GetLittleLData))]
        public void Format_LittleL(TextTestData<DateTime> data) => RunQuoteTest(data, new DateTimeJsonConverter('l'));
        [Theory]
        [MemberData(nameof(GetOData))]
        public void Format_O(TextTestData<DateTime> data) => RunQuoteTest(data, new DateTimeJsonConverter('O'));
    }

    public class DateTimeOffsetTests : BaseTest<DateTimeOffset>
    {
        public static IEnumerable<object[]> GetDefaultData() => Utf8.Tests.DateTimeOffsetTests.GetDefaultData();
        public static IEnumerable<object[]> GetRData() => Utf8.Tests.DateTimeOffsetTests.GetRData();
        public static IEnumerable<object[]> GetLittleLData() => Utf8.Tests.DateTimeOffsetTests.GetLittleLData();
        public static IEnumerable<object[]> GetOData() => Utf8.Tests.DateTimeOffsetTests.GetOData();

        [Theory]
        [MemberData(nameof(GetDefaultData))]
        public void Format_Default(TextTestData<DateTimeOffset> data) => RunQuoteTest(data, new DateTimeOffsetJsonConverter(default));
        [Theory]
        [MemberData(nameof(GetRData))]
        public void Format_R(TextTestData<DateTimeOffset> data) => RunQuoteTest(data, new DateTimeOffsetJsonConverter('R'));
        [Theory]
        [MemberData(nameof(GetLittleLData))]
        public void Format_LittleL(TextTestData<DateTimeOffset> data) => RunQuoteTest(data, new DateTimeOffsetJsonConverter('l'));
        [Theory]
        [MemberData(nameof(GetOData))]
        public void Format_O(TextTestData<DateTimeOffset> data) => RunQuoteTest(data, new DateTimeOffsetJsonConverter('O'));
    }

    public class TimeSpanTests : BaseTest<TimeSpan>
    {
        public static IEnumerable<object[]> GetLittleCData() => Utf8.Tests.TimeSpanTests.GetLittleCData();
        public static IEnumerable<object[]> GetGData() => Utf8.Tests.TimeSpanTests.GetGData();
        public static IEnumerable<object[]> GetLittleGData() => Utf8.Tests.TimeSpanTests.GetLittleGData();

        [Theory]
        [MemberData(nameof(GetLittleCData))]
        public void Format_LittleC(TextTestData<TimeSpan> data) => RunQuoteTest(data, new TimeSpanJsonConverter('c'));
        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_G(TextTestData<TimeSpan> data) => RunQuoteTest(data, new TimeSpanJsonConverter('G'));
        [Theory]
        [MemberData(nameof(GetLittleGData))]
        public void Format_LittleG(TextTestData<TimeSpan> data) => RunQuoteTest(data, new TimeSpanJsonConverter('g'));
    }
}
