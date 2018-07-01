using System;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfReader
    {
        public static bool TryReadDateTime(ref ReadOnlySpan<byte> remaining, out DateTime result, char standardFormat)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.StringExt:
                case EtfTokenType.BinaryExt:
                    {
                        if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                            return false;
                        return Utf8Reader.TryReadDateTime(ref bytes, out result, standardFormat);
                    }
                default:
                    return false;
            }
        }

        public static bool TryReadDateTimeOffset(ref ReadOnlySpan<byte> remaining, out DateTimeOffset result, char standardFormat)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.StringExt:
                case EtfTokenType.BinaryExt:
                    {
                        if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                            return false;
                        return Utf8Reader.TryReadDateTimeOffset(ref bytes, out result, standardFormat);
                    }
                default:
                    return false;
            }
        }

        public static bool TryReadTimeSpan(ref ReadOnlySpan<byte> remaining, out TimeSpan result, char standardFormat)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.StringExt:
                case EtfTokenType.BinaryExt:
                    {
                        if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                            return false;
                        return Utf8Reader.TryReadTimeSpan(ref bytes, out result, standardFormat);
                    }
                default:
                    return false;
            }
        }
    }
}
