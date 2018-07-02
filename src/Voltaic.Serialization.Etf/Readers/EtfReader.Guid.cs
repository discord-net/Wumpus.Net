using System;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfReader
    {
        public static bool TryReadGuid(ref ReadOnlySpan<byte> remaining, out Guid result, char standardFormat)
        {
            result = default;

            if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                return false;
            return Utf8Reader.TryReadGuid(ref bytes, out result, standardFormat);
        }
    }
}
