using System;
using System.Buffers.Text;

namespace Voltaic.Serialization.Utf8
{
    public static partial class Utf8Reader
    {
        public static bool TryReadDateTime(ref ReadOnlySpan<byte> remaining, out DateTime result, char standardFormat)
        {
            if (standardFormat == 'O')
            {
                if (!CustomUtf8Parser.TryParseDateTimeOffsetO(remaining, out var dtoResult, out int bytesConsumed, out var kind))
                {
                    result = default;
                    return false;
                }
                switch (kind)
                {
                    case DateTimeKind.Local:
                        result = dtoResult.LocalDateTime;
                        break;
                    case DateTimeKind.Utc:
                        result = dtoResult.UtcDateTime;
                        break;
                    default:
                        result = dtoResult.DateTime;
                        break;
                }
                remaining = remaining.Slice(bytesConsumed);
            }
            else
            {
                if (!Utf8Parser.TryParse(remaining, out result, out int bytesConsumed, standardFormat))
                    return false;
                remaining = remaining.Slice(bytesConsumed);
            }
            return true;
        }

        public static bool TryReadDateTimeOffset(ref ReadOnlySpan<byte> remaining, out DateTimeOffset result, char standardFormat)
        {
            if (standardFormat == 'O')
            {
                if (!CustomUtf8Parser.TryParseDateTimeOffsetO(remaining, out result, out int bytesConsumed, out _))
                    return false;
                remaining = remaining.Slice(bytesConsumed);
            }
            else
            {
                if (!Utf8Parser.TryParse(remaining, out result, out int bytesConsumed, standardFormat))
                    return false;
                remaining = remaining.Slice(bytesConsumed);
            }
            return true;
        }

        public static bool TryReadTimeSpan(ref ReadOnlySpan<byte> remaining, out TimeSpan result, char standardFormat)
        {
            if (!Utf8Parser.TryParse(remaining, out result, out int bytesConsumed, standardFormat))
                return false;
            remaining = remaining.Slice(bytesConsumed);
            return true;
        }
    }
}
