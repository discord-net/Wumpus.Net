using System;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfReader
    {
        public static bool TryReadGuid(ref ReadOnlySpan<byte> remaining, out Guid result, char standardFormat)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.StringExt:
                case EtfTokenType.BinaryExt:
                    {
                        if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                            return false;
                        return Utf8Reader.TryReadGuid(ref remaining, out result, standardFormat);
                    }
                default:
                    return false;
            }
        }
    }
}
