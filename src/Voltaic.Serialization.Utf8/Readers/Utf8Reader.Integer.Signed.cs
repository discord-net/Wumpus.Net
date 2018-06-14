using System;
using System.Buffers.Text;

namespace Voltaic.Serialization.Utf8
{
    public static partial class Utf8Reader
    {
        public static bool TryReadInt8(ref ReadOnlySpan<byte> remaining, out sbyte result)
        {
            if (!Utf8Parser.TryParse(remaining, out result, out int bytesConsumed))
                return false;
            remaining = remaining.Slice(bytesConsumed);
            return true;
        }

        public static bool TryReadInt16(ref ReadOnlySpan<byte> remaining, out short result)
        {
            if (!Utf8Parser.TryParse(remaining, out result, out int bytesConsumed))
                return false;
            remaining = remaining.Slice(bytesConsumed);
            return true;
        }

        public static bool TryReadInt32(ref ReadOnlySpan<byte> remaining, out int result)
        {
            if (!Utf8Parser.TryParse(remaining, out result, out int bytesConsumed))
                return false;
            remaining = remaining.Slice(bytesConsumed);
            return true;
        }

        public static bool TryReadInt64(ref ReadOnlySpan<byte> remaining, out long result)
        {
            if (!Utf8Parser.TryParse(remaining, out result, out int bytesConsumed))
                return false;
            remaining = remaining.Slice(bytesConsumed);
            return true;
        }
    }
}
