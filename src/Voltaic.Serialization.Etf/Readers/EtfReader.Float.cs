using System;
using System.Runtime.InteropServices;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfReader
    {
        public static bool TryReadSingle(ref ReadOnlySpan<byte> remaining, out float result, char standardFormat)
        {
            result = default;

            if (standardFormat != '\0')
            {
                if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                    return false;
                return Utf8Reader.TryReadSingle(ref bytes, out result, standardFormat);
            }

            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.Float:
                    // TODO: Untested
                    var bytes = remaining.Slice(1, 31);
                    remaining.Slice(32);
                    return Utf8Reader.TryReadSingle(ref bytes, out result, 'g');
                case EtfTokenType.NewFloat:
                    {
                        // TODO: Untested, does Discord have any endpoints that accept floats?
                        if (remaining.Length < 8)
                            return false;
                        remaining = remaining.Slice(1);
                        Span<double> dst = stackalloc double[1];
                        var dstBytes = MemoryMarshal.AsBytes(dst);

                        // Swap endian
                        dstBytes[0] = remaining[7];
                        dstBytes[1] = remaining[6];
                        dstBytes[2] = remaining[5];
                        dstBytes[3] = remaining[4];
                        dstBytes[4] = remaining[3];
                        dstBytes[5] = remaining[2];
                        dstBytes[6] = remaining[1];
                        dstBytes[7] = remaining[0];

                        result = (float)dst[0];
                        remaining = remaining.Slice(8);
                        return true;
                    }
                default:
                    return false;
            }
        }

        public static bool TryReadDouble(ref ReadOnlySpan<byte> remaining, out double result, char standardFormat)
        {
            result = default;

            if (standardFormat != '\0')
            {
                if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                    return false;
                return Utf8Reader.TryReadDouble(ref bytes, out result, standardFormat);
            }

            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.Float:
                    // TODO: Untested
                    var bytes = remaining.Slice(1, 31);
                    remaining.Slice(32);
                    return Utf8Reader.TryReadDouble(ref bytes, out result, 'g');
                case EtfTokenType.NewFloat:
                    {
                        // TODO: Untested, does Discord have any endpoints that accept floats?
                        if (remaining.Length < 8)
                            return false;
                        remaining = remaining.Slice(1);
                        Span<double> dst = stackalloc double[1];
                        var dstBytes = MemoryMarshal.AsBytes(dst);

                        // Swap endian
                        dstBytes[0] = remaining[7];
                        dstBytes[1] = remaining[6];
                        dstBytes[2] = remaining[5];
                        dstBytes[3] = remaining[4];
                        dstBytes[4] = remaining[3];
                        dstBytes[5] = remaining[2];
                        dstBytes[6] = remaining[1];
                        dstBytes[7] = remaining[0];

                        result = dst[0];
                        remaining = remaining.Slice(8);
                        return true;
                    }
                default:
                    return false;
            }
        }

        public static bool TryReadDecimal(ref ReadOnlySpan<byte> remaining, out decimal result, char standardFormat)
        {
            result = default;
            
            if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                return false;
            return Utf8Reader.TryReadDecimal(ref bytes, out result, standardFormat);
        }
    }
}
