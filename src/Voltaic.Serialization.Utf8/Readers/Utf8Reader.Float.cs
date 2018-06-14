using System;
using System.Buffers.Text;

namespace Voltaic.Serialization.Utf8
{
    public static partial class Utf8Reader
    {
        public static bool TryReadSingle(ref ReadOnlySpan<byte> remaining, out float result)
        {
            if (!Utf8Parser.TryParse(remaining, out result, out int bytesConsumed))
            {
                DebugLog.WriteFailure("Utf8Parser failed");
                return false;
            }
            remaining = remaining.Slice(bytesConsumed);
            return true;
        }

        public static bool TryReadDouble(ref ReadOnlySpan<byte> remaining, out double result)
        {
            if (!Utf8Parser.TryParse(remaining, out result, out int bytesConsumed))
            {
                DebugLog.WriteFailure("Utf8Parser failed");
                return false;
            }
            remaining = remaining.Slice(bytesConsumed);
            return true;
        }

        public static bool TryReadDecimal(ref ReadOnlySpan<byte> remaining, out decimal result)
        {
            if (!Utf8Parser.TryParse(remaining, out result, out int bytesConsumed))
            {
                DebugLog.WriteFailure("Utf8Parser failed");
                return false;
            }
            remaining = remaining.Slice(bytesConsumed);
            return true;
        }
    }
}
