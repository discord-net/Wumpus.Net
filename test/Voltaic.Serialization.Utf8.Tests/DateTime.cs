using System;
using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Utf8.Tests
{
    public class DateTimeTests : BaseTest<DateTime>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return Fail("0000-12-31T12:59:59.9990000Z"); // Min - 1ms
            yield return ReadWrite("0001-01-01T00:00:00.0000000Z",
                new DateTimeOffset(year: 1, month: 1, day: 1, hour: 0, minute: 0, second: 0, millisecond: 0, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)).UtcDateTime); // Min
            yield return ReadWrite("2017-01-13T03:45:32.0550000Z",
                new DateTimeOffset(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32, millisecond: 55, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)).UtcDateTime);
            yield return Read("2017-01-13T03:45:32.0550000",
                new DateTimeOffset(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32, millisecond: 55, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)).UtcDateTime);
            yield return ReadWrite("9999-12-31T11:59:59.9990000Z",
                new DateTimeOffset(year: 9999, month: 12, day: 31, hour: 11, minute: 59, second: 59, millisecond: 999, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)).UtcDateTime); // Max
            yield return Fail("10000-00-00T00:00:00.0000000Z"); // Max + 1ms
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void TestDateTime(TestData data) => Test(data);
    }

    public class DateTimeOffsetTests : BaseTest<DateTimeOffset>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return Fail("0000-12-31T12:59:59.9990000Z"); // Min - 1ms
            yield return Read( //Min
                "0001-01-01T00:00:00.0000000Z",
                new DateTimeOffset(year: 1, month: 1, day: 1, hour: 0, minute: 0, second: 0, millisecond: 0, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)));
            yield return ReadWrite( //Min
                "0001-01-01T00:00:00.0000000+00:00",
                new DateTimeOffset(year: 1, month: 1, day: 1, hour: 0, minute: 0, second: 0, millisecond: 0, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)));
            yield return ReadWrite(
                "2017-01-13T03:45:32.0550000+08:00",
                new DateTimeOffset(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32, millisecond: 55, offset:
                new TimeSpan(hours: 8, minutes: 0, seconds: 0)));
            yield return ReadWrite(
                "2017-01-13T03:45:32.0550000-09:00",
                new DateTimeOffset(year: 2017, month: 1, day: 13, hour: 3, minute: 45, second: 32, millisecond: 55, offset:
                new TimeSpan(hours: -9, minutes: 0, seconds: 0)));
            yield return ReadWrite( //Max
                "9999-12-31T11:59:59.9990000+00:00",
                new DateTimeOffset(year: 9999, month: 12, day: 31, hour: 11, minute: 59, second: 59, millisecond: 999, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)));
            yield return Read( //Max
                "9999-12-31T11:59:59.9990000Z",
                new DateTimeOffset(year: 9999, month: 12, day: 31, hour: 11, minute: 59, second: 59, millisecond: 999, offset:
                new TimeSpan(hours: 0, minutes: 0, seconds: 0)));
            yield return Fail("10000-00-00T00:00:00.0000000Z"); // Max + 1ms
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void TestDateTimeOffset(TestData data) => Test(data);
    }

    public class TimeSpanTests : BaseTest<TimeSpan>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return Fail("-10675199.02:48:05.4775809"); // Min - 1
            yield return ReadWrite("-10675199.02:48:05.4775808", new TimeSpan(ticks: -9223372036854775808)); // Min
            yield return ReadWrite("-00:00:00.0000001", new TimeSpan(ticks: -1));
            yield return ReadWrite("00:00:00", new TimeSpan(ticks: 0));
            yield return ReadWrite("00:00:00.0000001", new TimeSpan(ticks: 1));
            yield return ReadWrite("10675199.02:48:05.4775807", new TimeSpan(ticks: 9223372036854775807)); // Max
            yield return Fail("10675199.02:48:05.4775808"); // Max + 1
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void TestTimeSpan(TestData data) => Test(data);
    }
}
