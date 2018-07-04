using System;
using System.Buffers.Text;

namespace Voltaic.Serialization.Utf8
{
    // https://github.com/dotnet/corefx/blob/master/src/System.Memory/src/System/Buffers/Text/Utf8Parser/Utf8Parser.Date.cs
    // https://github.com/dotnet/corefx/blob/master/src/System.Memory/src/System/Buffers/Text/Utf8Parser/Utf8Parser.Date.O.cs
    // https://github.com/dotnet/corefx/blob/master/src/System.Memory/src/System/Buffers/Text/Utf8Constants.cs
    internal class CustomUtf8Parser
    {
        // Modifies Utf8Parser to add support for variable-length fractions
        public static bool TryParseDateTimeOffsetO(ReadOnlySpan<byte> source, out DateTimeOffset value, out int bytesConsumed, out DateTimeKind kind)
        {
            if (source.Length < 19)
            {
                value = default;
                bytesConsumed = 0;
                kind = default;
                return false;
            }

            int year;
            {
                uint digit1 = source[0] - 48u; // '0'
                uint digit2 = source[1] - 48u; // '0'
                uint digit3 = source[2] - 48u; // '0'
                uint digit4 = source[3] - 48u; // '0'

                if (digit1 > 9 || digit2 > 9 || digit3 > 9 || digit4 > 9)
                {
                    value = default;
                    bytesConsumed = 0;
                    kind = default;
                    return false;
                }

                year = (int)(digit1 * 1000 + digit2 * 100 + digit3 * 10 + digit4);
            }

            if (source[4] != '-')
            {
                value = default;
                bytesConsumed = 0;
                kind = default;
                return false;
            }

            int month;
            {
                uint digit1 = source[5] - 48u; // '0'
                uint digit2 = source[6] - 48u; // '0'

                if (digit1 > 9 || digit2 > 9)
                {
                    value = default;
                    bytesConsumed = 0;
                    kind = default;
                    return false;
                }

                month = (int)(digit1 * 10 + digit2);
            }

            if (source[7] != '-')
            {
                value = default;
                bytesConsumed = 0;
                kind = default;
                return false;
            }

            int day;
            {
                uint digit1 = source[8] - 48u; // '0'
                uint digit2 = source[9] - 48u; // '0'

                if (digit1 > 9 || digit2 > 9)
                {
                    value = default;
                    bytesConsumed = 0;
                    kind = default;
                    return false;
                }

                day = (int)(digit1 * 10 + digit2);
            }

            if (source[10] != 'T')
            {
                value = default;
                bytesConsumed = 0;
                kind = default;
                return false;
            }

            int hour;
            {
                uint digit1 = source[11] - 48u; // '0'
                uint digit2 = source[12] - 48u; // '0'

                if (digit1 > 9 || digit2 > 9)
                {
                    value = default;
                    bytesConsumed = 0;
                    kind = default;
                    return false;
                }

                hour = (int)(digit1 * 10 + digit2);
            }

            if (source[13] != ':')
            {
                value = default;
                bytesConsumed = 0;
                kind = default;
                return false;
            }

            int minute;
            {
                uint digit1 = source[14] - 48u; // '0'
                uint digit2 = source[15] - 48u; // '0'

                if (digit1 > 9 || digit2 > 9)
                {
                    value = default;
                    bytesConsumed = 0;
                    kind = default;
                    return false;
                }

                minute = (int)(digit1 * 10 + digit2);
            }

            if (source[16] != ':')
            {
                value = default;
                bytesConsumed = 0;
                kind = default;
                return false;
            }

            int second;
            {
                uint digit1 = source[17] - 48u; // '0'
                uint digit2 = source[18] - 48u; // '0'

                if (digit1 > 9 || digit2 > 9)
                {
                    value = default;
                    bytesConsumed = 0;
                    kind = default;
                    return false;
                }

                second = (int)(digit1 * 10 + digit2);
            }

            // Fraction is both optional and can have a dynamic size but Utf8Parser assumes it's always 7 digits
            int fractionDigits;
            int fraction;
            if (source.Length >= 20 && source[19] == '.')
            {
                source = source.Slice(20);
                if (!Utf8Parser.TryParse(source, out fraction, out fractionDigits, 'D'))
                {
                    value = default;
                    bytesConsumed = 0;
                    kind = default;
                    return false;
                }
                for (int i = fractionDigits; i < 7; i++)
                    fraction *= 10;
                source = source.Slice(fractionDigits);
                fractionDigits++;
            }
            else
            {
                source = source.Slice(19);
                fractionDigits = 0;
                fraction = 0;
            }

            byte offsetChar = (source.Length == 0) ? default : source[0]; // 27
            if (offsetChar != 'Z' && offsetChar != '+' && offsetChar != '-')
            {
                if (!TryCreateDateTimeOffsetInterpretingDataAsLocalTime(year: year, month: month, day: day, hour: hour, minute: minute, second: second, fraction: fraction, out value))
                {
                    value = default;
                    bytesConsumed = 0;
                    kind = default;
                    return false;
                }
                bytesConsumed = 19 + fractionDigits;
                kind = DateTimeKind.Unspecified;
                return true;
            }

            if (offsetChar == 'Z')
            {
                // Same as specifying an offset of "+00:00", except that DateTime's Kind gets set to UTC rather than Local
                if (!TryCreateDateTimeOffset(year: year, month: month, day: day, hour: hour, minute: minute, second: second, fraction: fraction, offsetNegative: false, offsetHours: 0, offsetMinutes: 0, out value))
                {
                    value = default;
                    bytesConsumed = 0;
                    kind = default;
                    return false;
                }

                bytesConsumed = 20 + fractionDigits;
                kind = DateTimeKind.Utc;
                return true;
            }

            if (source.Length < 6)
            {
                value = default;
                bytesConsumed = 0;
                kind = default;
                return false;
            }

            int offsetHours;
            {
                uint digit1 = source[1] - 48u; // '0'
                uint digit2 = source[2] - 48u; // '0'

                if (digit1 > 9 || digit2 > 9)
                {
                    value = default;
                    bytesConsumed = 0;
                    kind = default;
                    return false;
                }

                offsetHours = (int)(digit1 * 10 + digit2);
            }

            if (source[3] != ':')
            {
                value = default;
                bytesConsumed = 0;
                kind = default;
                return false;
            }

            int offsetMinutes;
            {
                uint digit1 = source[4] - 48u; // '0'
                uint digit2 = source[5] - 48u; // '0'

                if (digit1 > 9 || digit2 > 9)
                {
                    value = default;
                    bytesConsumed = 0;
                    kind = default;
                    return false;
                }

                offsetMinutes = (int)(digit1 * 10 + digit2);
            }

            if (!TryCreateDateTimeOffset(year: year, month: month, day: day, hour: hour, minute: minute, second: second, fraction: fraction, offsetNegative: offsetChar == '-', offsetHours: offsetHours, offsetMinutes: offsetMinutes, out value))
            {
                value = default;
                bytesConsumed = 0;
                kind = default;
                return false;
            }

            bytesConsumed = 25 + fractionDigits;
            kind = DateTimeKind.Local;
            return true;
        }

        /// <summary>
        /// Overflow-safe DateTimeOffset factory.
        /// </summary>
        private static bool TryCreateDateTimeOffset(DateTime dateTime, bool offsetNegative, int offsetHours, int offsetMinutes, out DateTimeOffset value)
        {
            if (((uint)offsetHours) > 14)
            {
                value = default;
                return false;
            }

            if (((uint)offsetMinutes) > 59)
            {
                value = default;
                return false;
            }

            if (offsetHours == 14 && offsetMinutes != 0)
            {
                value = default;
                return false;
            }

            long offsetTicks = (((long)offsetHours) * 3600 + ((long)offsetMinutes) * 60) * TimeSpan.TicksPerSecond;
            if (offsetNegative)
            {
                offsetTicks = -offsetTicks;
            }

            try
            {
                value = new DateTimeOffset(ticks: dateTime.Ticks, offset: new TimeSpan(ticks: offsetTicks));
            }
            catch (ArgumentOutOfRangeException)
            {
                // If we got here, the combination of the DateTime + UTC offset strayed outside the 1..9999 year range. This case seems rare enough
                // that it's better to catch the exception rather than replicate DateTime's range checking (which it's going to do anyway.)
                value = default;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Overflow-safe DateTimeOffset factory.
        /// </summary>
        private static bool TryCreateDateTimeOffset(int year, int month, int day, int hour, int minute, int second, int fraction, bool offsetNegative, int offsetHours, int offsetMinutes, out DateTimeOffset value)
        {
            if (!TryCreateDateTime(year: year, month: month, day: day, hour: hour, minute: minute, second: second, fraction: fraction, kind: DateTimeKind.Unspecified, out DateTime dateTime))
            {
                value = default;
                return false;
            }

            if (!TryCreateDateTimeOffset(dateTime: dateTime, offsetNegative: offsetNegative, offsetHours: offsetHours, offsetMinutes: offsetMinutes, out value))
            {
                value = default;
                return false;
            }

            return true;
        }

        private static bool TryCreateDateTimeOffsetInterpretingDataAsLocalTime(int year, int month, int day, int hour, int minute, int second, int fraction, out DateTimeOffset value)
        {
            if (!TryCreateDateTime(year: year, month: month, day: day, hour: hour, minute: minute, second: second, fraction: fraction, DateTimeKind.Local, out DateTime dateTime))
            {
                value = default;
                return false;
            }

            try
            {
                value = new DateTimeOffset(dateTime);
            }
            catch (ArgumentOutOfRangeException)
            {
                // If we got here, the combination of the DateTime + UTC offset strayed outside the 1..9999 year range. This case seems rare enough
                // that it's better to catch the exception rather than replicate DateTime's range checking (which it's going to do anyway.)

                value = default;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Overflow-safe DateTime factory.
        /// </summary>
        private static bool TryCreateDateTime(int year, int month, int day, int hour, int minute, int second, int fraction, DateTimeKind kind, out DateTime value)
        {
            if (year == 0)
            {
                value = default;
                return false;
            }

            if ((((uint)month) - 1) >= 12)
            {
                value = default;
                return false;
            }

            uint dayMinusOne = ((uint)day) - 1;
            if (dayMinusOne >= 28 && dayMinusOne >= DateTime.DaysInMonth(year, month))
            {
                value = default;
                return false;
            }

            if (((uint)hour) > 23)
            {
                value = default;
                return false;
            }

            if (((uint)minute) > 59)
            {
                value = default;
                return false;
            }

            if (((uint)second) > 59)
            {
                value = default;
                return false;
            }

            int[] days = DateTime.IsLeapYear(year) ? s_daysToMonth366 : s_daysToMonth365;
            int yearMinusOne = year - 1;
            int totalDays = (yearMinusOne * 365) + (yearMinusOne / 4) - (yearMinusOne / 100) + (yearMinusOne / 400) + days[month - 1] + day - 1;
            long ticks = totalDays * TimeSpan.TicksPerDay;
            int totalSeconds = (hour * 3600) + (minute * 60) + second;
            ticks += totalSeconds * TimeSpan.TicksPerSecond;
            ticks += fraction;
            value = new DateTime(ticks: ticks, kind: kind);
            return true;
        }

        private static readonly int[] s_daysToMonth365 = { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365 };
        private static readonly int[] s_daysToMonth366 = { 0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335, 366 };
    }
}
