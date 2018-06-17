using System;
using System.Buffers.Text;

namespace Voltaic.Serialization.Utf8
{
    public static partial class Utf8Reader
    {
        public static bool TryReadBoolean(ref ReadOnlySpan<byte> remaining, out bool result, char standardFormat)
        {
            if (!Utf8Parser.TryParse(remaining, out result, out int bytesConsumed, standardFormat))
                return false;
            remaining = remaining.Slice(bytesConsumed);
            return true;
        }
    }
}
