using System;

namespace Wumpus
{
    //Source: https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/DateTimeOffset.cs
    internal static class DateTimeUtils
    {
        private const long _unixEpochTicks = 621_355_968_000_000_000;
        private const long _unixEpochSeconds = 62_135_596_800;
        private const long _unixEpochMilliseconds = 62_135_596_800_000;

        public static DateTimeOffset FromSnowflake(ulong value)
            => FromUnixMilliseconds((long)((value >> 22) + 1420070400000UL));
        public static ulong ToSnowflake(DateTimeOffset value)
            => ((ulong)ToUnixMilliseconds(value) - 1420070400000UL) << 22;

        public static DateTimeOffset FromTicks(long ticks)
            => new DateTimeOffset(ticks, TimeSpan.Zero);
        public static DateTimeOffset? FromTicks(long? ticks)
            => ticks != null ? new DateTimeOffset(ticks.Value, TimeSpan.Zero) : (DateTimeOffset?)null;

        public static DateTimeOffset FromUnixSeconds(long seconds)
        {
            long ticks = seconds * TimeSpan.TicksPerSecond + _unixEpochTicks;
            return new DateTimeOffset(ticks, TimeSpan.Zero);
        }
        public static DateTimeOffset FromUnixMilliseconds(long milliseconds)
        {
            long ticks = milliseconds * TimeSpan.TicksPerMillisecond + _unixEpochTicks;
            return new DateTimeOffset(ticks, TimeSpan.Zero);
        }

        public static long ToUnixSeconds(DateTimeOffset dto)
        {
            long seconds = dto.UtcDateTime.Ticks / TimeSpan.TicksPerSecond;
            return seconds - _unixEpochSeconds;
        }
        public static long ToUnixMilliseconds(DateTimeOffset dto)
        {
            long milliseconds = dto.UtcDateTime.Ticks / TimeSpan.TicksPerMillisecond;
            return milliseconds - _unixEpochMilliseconds;
        }
    }
}
