using System;
using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Utf8.Tests
{
    public class DateTimeTests : BaseTest<DateTime>
    {
        public static IEnumerable<object[]> GetDefaultData()
        {
            yield return FailRead("31/12/0000 12:59:59 +00:00"); // Min - 1s
            yield return Read("01/01/0001 00:00:00 +00:00", //Min
                new DateTime(year: 1, month: 1, day: 1, hour: 0, minute: 0, second: 0));
            yield return Read("01/01/0001 00:00:00 +00:00", //Min
                new DateTime(year: 1, month: 1, day: 1, hour: 0, minute: 0, second: 0));
            yield return Read("01/13/2017 03:45:32 +00:00",
                new DateTime(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32));
            yield return Read("01/13/2017 03:45:32 +01:00",
                new DateTime(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32));
            yield return Read("12/31/9999 11:59:59 +00:00", //Max
                new DateTime(year: 9999, month: 12, day: 31, hour: 11, minute: 59, second: 59));
            yield return FailRead("01/01/10000 00:00:00 +00:00"); // Max + 1s

            foreach (var test in GetGData())
                yield return test;
        }

        public static IEnumerable<object[]> GetGData()
        {
            yield return FailRead("31/12/0000 12:59:59"); // Min - 1s
            yield return ReadWrite("01/01/0001 00:00:00", //Min
                new DateTime(year: 1, month: 1, day: 1, hour: 0, minute: 0, second: 0));
            yield return ReadWrite("01/13/2017 03:45:32",
                new DateTime(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32));
            yield return ReadWrite("01/13/2017 03:45:32",
                new DateTime(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32));
            yield return ReadWrite("12/31/9999 11:59:59", //Max
                new DateTime(year: 9999, month: 12, day: 31, hour: 11, minute: 59, second: 59));
            yield return FailRead("01/01/10000 00:00:00"); // Max + 1s
        }

        public static IEnumerable<object[]> GetRData()
        {
            yield return FailRead("Sun, 00 Dec 0000 12:59:59 GMT"); // Min - 1s
            yield return ReadWrite( //Min
                "Mon, 01 Jan 0001 00:00:00 GMT",
                new DateTime(year: 1, month: 1, day: 1, hour: 0, minute: 0, second: 0));
            yield return ReadWrite(
                "Fri, 13 Jan 2017 03:45:32 GMT",
                new DateTime(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32));
            yield return ReadWrite(
                "Fri, 13 Jan 2017 03:45:32 GMT",
                new DateTime(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32));
            yield return ReadWrite( //Max
                "Fri, 31 Dec 9999 11:59:59 GMT",
                new DateTime(year: 9999, month: 12, day: 31, hour: 11, minute: 59, second: 59));
            yield return FailRead("Sat, 01 Jan 10000 00:00:00 GMT"); // Max + 1s
        }

        public static IEnumerable<object[]> GetLittleLData()
        {
            yield return FailRead("sun, 00 dec 0000 12:59:59 gmt"); // Min - 1s
            yield return ReadWrite( //Min
                "mon, 01 jan 0001 00:00:00 gmt",
                new DateTime(year: 1, month: 1, day: 1, hour: 0, minute: 0, second: 0));
            yield return ReadWrite(
                "fri, 13 jan 2017 03:45:32 gmt",
                new DateTime(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32));
            yield return ReadWrite(
                "fri, 13 jan 2017 03:45:32 gmt",
                new DateTime(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32));
            yield return ReadWrite( //Max
                "fri, 31 dec 9999 11:59:59 gmt",
                new DateTime(year: 9999, month: 12, day: 31, hour: 11, minute: 59, second: 59));
            yield return FailRead("sat, 01 jan 10000 00:00:00 gmt"); // Max + 1s
        }

        public static IEnumerable<object[]> GetOData()
        {
            yield return FailRead("0000-12-31T12:59:59.9990000"); // Min - 1ms
            yield return ReadWrite("0001-01-01T00:00:00.0000000",
                new DateTime(year: 1, month: 1, day: 1, hour: 0, minute: 0, second: 0, millisecond: 0)); // Min
            yield return ReadWrite("2017-01-13T03:45:32.0500000",
                new DateTime(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32, millisecond: 50));
            yield return Read("2017-01-13T03:45:32.050000",
                new DateTime(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32, millisecond: 50)); // Shorter fraction
            yield return Read("2017-01-13T03:45:32",
                new DateTime(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32, millisecond: 0)); // Missing fraction
            yield return ReadWrite("9999-12-31T11:59:59.9990000",
                new DateTime(year: 9999, month: 12, day: 31, hour: 11, minute: 59, second: 59, millisecond: 999)); // Max
            yield return FailRead("10000-00-00T00:00:00.0000000"); // Max + 1ms
        }

        [Theory]
        [MemberData(nameof(GetDefaultData))]
        public void Format_Default(TextTestData<DateTime> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_G(TextTestData<DateTime> data) => RunTest(data, new DateTimeUtf8Converter('G'));
        [Theory]
        [MemberData(nameof(GetRData))]
        public void Format_R(TextTestData<DateTime> data) => RunTest(data, new DateTimeUtf8Converter('R'));
        [Theory]
        [MemberData(nameof(GetLittleLData))]
        public void Format_LittleL(TextTestData<DateTime> data) => RunTest(data, new DateTimeUtf8Converter('l'));
        [Theory]
        [MemberData(nameof(GetOData))]
        public void Format_O(TextTestData<DateTime> data) => RunTest(data, new DateTimeUtf8Converter('O'));
    }

    public class DateTimeOffsetTests : BaseTest<DateTimeOffset>
    {
        // Corefx's comments say G == default, but G actually redirects to DateTime's G and assumes local time
        public static IEnumerable<object[]> GetDefaultData()
        {
            yield return FailRead("12/31/0000 12:59:59 +00:00"); // Min - 1ms
            yield return ReadWrite("01/01/0001 00:00:00 +00:00", //Min
                new DateTimeOffset(year: 1, month: 1, day: 1, hour: 0, minute: 0, second: 0, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)));
            yield return ReadWrite("01/13/2017 03:45:32 +08:00",
                new DateTimeOffset(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32, offset:
                new TimeSpan(hours: 8, minutes: 0, seconds: 0)));
            yield return ReadWrite("01/13/2017 03:45:32 -09:00",
                new DateTimeOffset(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32, offset:
                new TimeSpan(hours: -9, minutes: 0, seconds: 0)));
            yield return ReadWrite("12/31/9999 11:59:59 +00:00", //Max
                new DateTimeOffset(year: 9999, month: 12, day: 31, hour: 11, minute: 59, second: 59, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)));
            yield return FailRead("00/00/10000 00:00:00 +00:00"); // Max + 1ms
        }

        public static IEnumerable<object[]> GetRData()
        {
            yield return FailRead("Sun, 00 Dec 0000 12:59:59 GMT"); // Min - 1s
            yield return ReadWrite( //Min
                "Mon, 01 Jan 0001 00:00:00 GMT",
                new DateTimeOffset(year: 1, month: 1, day: 1, hour: 0, minute: 0, second: 0, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)));
            yield return ReadWrite(
                "Fri, 13 Jan 2017 03:45:32 GMT",
                new DateTimeOffset(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)));
            yield return ReadWrite(
                "Fri, 13 Jan 2017 03:45:32 GMT",
                new DateTimeOffset(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)));
            yield return ReadWrite( //Max
                "Fri, 31 Dec 9999 11:59:59 GMT",
                new DateTimeOffset(year: 9999, month: 12, day: 31, hour: 11, minute: 59, second: 59, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)));
            yield return FailRead("Sat, 01 Jan 10000 00:00:00 GMT"); // Max + 1s
        }

        public static IEnumerable<object[]> GetLittleLData()
        {
            yield return FailRead("sun, 00 dec 0000 12:59:59 gmt"); // Min - 1s
            yield return ReadWrite( //Min
                "mon, 01 jan 0001 00:00:00 gmt",
                new DateTimeOffset(year: 1, month: 1, day: 1, hour: 0, minute: 0, second: 0, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)));
            yield return ReadWrite(
                "fri, 13 jan 2017 03:45:32 gmt",
                new DateTimeOffset(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)));
            yield return ReadWrite(
                "fri, 13 jan 2017 03:45:32 gmt",
                new DateTimeOffset(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)));
            yield return ReadWrite( //Max
                "fri, 31 dec 9999 11:59:59 gmt",
                new DateTimeOffset(year: 9999, month: 12, day: 31, hour: 11, minute: 59, second: 59, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)));
            yield return FailRead("sat, 01 jan 10000 00:00:00 gmt"); // Max + 1s
        }

        public static IEnumerable<object[]> GetOData()
        {
            yield return FailRead("0000-12-31T12:59:59.9990000Z"); // Min - 1ms
            yield return Read("0001-01-01T00:00:00.0000000Z", //Min
                new DateTimeOffset(year: 1, month: 1, day: 1, hour: 0, minute: 0, second: 0, millisecond: 0, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)));
            yield return ReadWrite("0001-01-01T00:00:00.0000000+00:00", //Min
                new DateTimeOffset(year: 1, month: 1, day: 1, hour: 0, minute: 0, second: 0, millisecond: 0, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)));
            yield return ReadWrite("2017-01-13T03:45:32.0500000+08:00",
                new DateTimeOffset(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32, millisecond: 50, offset:
                new TimeSpan(hours: 8, minutes: 0, seconds: 0)));
            yield return ReadWrite("2017-01-13T03:45:32.0500000-09:00",
                new DateTimeOffset(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32, millisecond: 50, offset:
                new TimeSpan(hours: -9, minutes: 0, seconds: 0)));
            yield return Read("2017-01-13T03:45:32.050000-09:00", // Shorter fraction
                new DateTimeOffset(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32, millisecond: 50, offset:
                new TimeSpan(hours: -9, minutes: 0, seconds: 0)));
            yield return Read("2017-01-13T03:45:32-09:00", // No fraction
                new DateTimeOffset(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32, millisecond: 0, offset:
                new TimeSpan(hours: -9, minutes: 0, seconds: 0)));
            yield return ReadWrite("9999-12-31T11:59:59.9990000+00:00", //Max
                new DateTimeOffset(year: 9999, month: 12, day: 31, hour: 11, minute: 59, second: 59, millisecond: 999, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)));
            yield return Read("9999-12-31T11:59:59.9990000Z", //Max
                new DateTimeOffset(year: 9999, month: 12, day: 31, hour: 11, minute: 59, second: 59, millisecond: 999, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)));
            yield return FailRead("10000-00-00T00:00:00.0000000Z"); // Max + 1ms
        }

        [Theory]
        [MemberData(nameof(GetDefaultData))]
        public void Format_G(TextTestData<DateTimeOffset> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetRData))]
        public void Format_R(TextTestData<DateTimeOffset> data) => RunTest(data, new DateTimeOffsetUtf8Converter('R'));
        [Theory]
        [MemberData(nameof(GetLittleLData))]
        public void Format_LittleL(TextTestData<DateTimeOffset> data) => RunTest(data, new DateTimeOffsetUtf8Converter('l'));
        [Theory]
        [MemberData(nameof(GetOData))]
        public void Format_O(TextTestData<DateTimeOffset> data) => RunTest(data, new DateTimeOffsetUtf8Converter('O'));
    }

    public class TimeSpanTests : BaseTest<TimeSpan>
    {
        public static IEnumerable<object[]> GetLittleCData()
        {
            yield return FailRead("-10675199.02:48:05.4775809"); // Min - 1
            yield return ReadWrite("-10675199.02:48:05.4775808", new TimeSpan(ticks: -9223372036854775808)); // Min
            yield return ReadWrite("-00:00:00.0000001", new TimeSpan(ticks: -1));
            yield return ReadWrite("00:00:00", new TimeSpan(ticks: 0));
            yield return ReadWrite("00:00:00.0000001", new TimeSpan(ticks: 1));
            yield return ReadWrite("10675199.02:48:05.4775807", new TimeSpan(ticks: 9223372036854775807)); // Max
            yield return FailRead("10675199.02:48:05.4775808"); // Max + 1
        }
        public static IEnumerable<object[]> GetGData()
        {
            yield return FailRead("-10675199:02:48:05.4775809"); // Min - 1
            yield return ReadWrite("-10675199:02:48:05.4775808", new TimeSpan(ticks: -9223372036854775808)); // Min
            yield return ReadWrite("-0:00:00:00.0000001", new TimeSpan(ticks: -1));
            yield return ReadWrite("0:00:00:00.0000000", new TimeSpan(ticks: 0));
            yield return ReadWrite("0:00:00:00.0000001", new TimeSpan(ticks: 1));
            yield return ReadWrite("10675199:02:48:05.4775807", new TimeSpan(ticks: 9223372036854775807)); // Max
            yield return FailRead("10675199:02:48:05.4775808"); // Max + 1
        }
        public static IEnumerable<object[]> GetLittleGData()
        {
            yield return FailRead("-10675199:02:48:05.4775809"); // Min - 1
            yield return ReadWrite("-10675199:2:48:05.4775808", new TimeSpan(ticks: -9223372036854775808)); // Min
            yield return ReadWrite("-0:00:00.0000001", new TimeSpan(ticks: -1));
            yield return ReadWrite("0:00:00", new TimeSpan(ticks: 0));
            yield return ReadWrite("0:00:00.0000001", new TimeSpan(ticks: 1));
            yield return ReadWrite("10675199:2:48:05.4775807", new TimeSpan(ticks: 9223372036854775807)); // Max
            yield return FailRead("10675199:02:48:05.4775808"); // Max + 1
        }

        [Theory]
        [MemberData(nameof(GetLittleCData))]
        public void Format_LittleC(TextTestData<TimeSpan> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_G(TextTestData<TimeSpan> data) => RunTest(data, new TimeSpanUtf8Converter('G'));
        [Theory]
        [MemberData(nameof(GetLittleGData))]
        public void Format_LittleG(TextTestData<TimeSpan> data) => RunTest(data, new TimeSpanUtf8Converter('g'));
    }
}
