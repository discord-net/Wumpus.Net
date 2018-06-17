using System;
using System.Buffers.Text;

namespace Voltaic.Serialization.Utf8
{
    public static partial class Utf8Reader
    {
        public static bool TryReadUInt8(ref ReadOnlySpan<byte> remaining, out byte result, char standardFormat)
        {
            if (!Utf8Parser.TryParse(remaining, out result, out int bytesConsumed, standardFormat))
                return false;
            remaining = remaining.Slice(bytesConsumed);
            return true;
        }

        public static bool TryReadUInt16(ref ReadOnlySpan<byte> remaining, out ushort result, char standardFormat)
        {
            if (!Utf8Parser.TryParse(remaining, out result, out int bytesConsumed, standardFormat))
                return false;
            remaining = remaining.Slice(bytesConsumed);
            return true;
        }

        public static bool TryReadUInt32(ref ReadOnlySpan<byte> remaining, out uint result, char standardFormat)
        {
            if (!Utf8Parser.TryParse(remaining, out result, out int bytesConsumed, standardFormat))
                return false;
            remaining = remaining.Slice(bytesConsumed);
            return true;
        }

        public static bool TryReadUInt64(ref ReadOnlySpan<byte> remaining, out ulong result, char standardFormat)
        {
            if (!Utf8Parser.TryParse(remaining, out result, out int bytesConsumed, standardFormat))
                return false;
            remaining = remaining.Slice(bytesConsumed);
            return true;
        }
    }
}
