using System;
using System.Buffers.Text;

namespace Voltaic.Serialization.Utf8
{
    public static partial class Utf8Reader
    {
        public static bool TryReadDateTime(ref ReadOnlySpan<byte> remaining, out DateTime result)
        {
            if (!Utf8Parser.TryParse(remaining, out result, out int bytesConsumed, 'O'))
                return false;
            remaining = remaining.Slice(bytesConsumed);
            return true;
        }

        public static bool TryReadDateTimeOffset(ref ReadOnlySpan<byte> remaining, out DateTimeOffset result)
        {
            if (!Utf8Parser.TryParse(remaining, out result, out int bytesConsumed, 'O'))
                return false;
            remaining = remaining.Slice(bytesConsumed);
            return true;
        }

        public static bool TryReadTimeSpan(ref ReadOnlySpan<byte> remaining, out TimeSpan result)
        {
            if (!Utf8Parser.TryParse(remaining, out result, out int bytesConsumed))
                return false;
            remaining = remaining.Slice(bytesConsumed);
            return true;
        }
    }
}
