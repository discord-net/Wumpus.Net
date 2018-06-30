using System;
using System.Buffers.Binary;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfReader
    {
        public static bool TryReadChar(ref ReadOnlySpan<byte> remaining, out char result)
        {
            result = default;

            if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                return false;
            return Utf8Reader.TryReadChar(ref bytes, out result);
        }

        public static bool TryReadString(ref ReadOnlySpan<byte> remaining, out string result)
        {
            result = default;

            if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                return false;
            return Utf8Reader.TryReadString(ref bytes, out result);
        }

        public static bool TryReadUtf8String(ref ReadOnlySpan<byte> remaining, out Utf8String result)
        {
            result = default;

            if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                return false;
            return Utf8Reader.TryReadUtf8String(ref bytes, out result);
        }

        public static bool TryReadUtf8Bytes(ref ReadOnlySpan<byte> remaining, out ReadOnlySpan<byte> result)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.StringExt:
                    {
                        if (remaining.Length < 2)
                            return false;
                        remaining = remaining.Slice(1);
                        ushort length = BinaryPrimitives.ReadUInt16BigEndian(remaining);
                        remaining = remaining.Slice(2);

                        if (remaining.Length < length)
                            return false;
                        result = remaining.Slice(0, length);
                        remaining = remaining.Slice(length);
                        return true;
                    }
                case EtfTokenType.BinaryExt:
                    {
                        if (remaining.Length < 4)
                            return false;
                        remaining = remaining.Slice(1);
                        uint length = BinaryPrimitives.ReadUInt32BigEndian(remaining);
                        remaining = remaining.Slice(4);

                        if (remaining.Length < length)
                            return false;
                        result = remaining.Slice(0, (int)length);
                        remaining = remaining.Slice((int)length);
                        return true;
                    }
                default:
                    return false;
            }
        }

        public static bool TryReadUtf16Key(ref ReadOnlySpan<byte> remaining, out string result)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.AtomExt:
                case EtfTokenType.AtomUtf8Ext:
                    {
                        if (remaining.Length < 3)
                            return false;
                        remaining = remaining.Slice(1);
                        ushort length = BinaryPrimitives.ReadUInt16BigEndian(remaining);
                        remaining = remaining.Slice(2);

                        if (remaining.Length < length)
                            return false;
                        var span = remaining.Slice(0, length);
                        remaining = remaining.Slice(length);
                        return Utf8Reader.TryReadString(ref span, out result);
                    }
                case EtfTokenType.SmallAtomExt:
                case EtfTokenType.SmallAtomUtf8Ext:
                    {
                        if (remaining.Length < 3)
                            return false;
                        //remaining = remaining.Slice(1);
                        byte length = remaining[1];
                        remaining = remaining.Slice(2);

                        if (remaining.Length < length)
                            return false;
                        var span = remaining.Slice(0, length);
                        remaining = remaining.Slice(length);
                        return Utf8Reader.TryReadString(ref span, out result);
                    }
                default:
                    return false;
            }
        }

        public static bool TryReadUtf8Key(ref ReadOnlySpan<byte> remaining, out Utf8String result)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.AtomExt:
                case EtfTokenType.AtomUtf8Ext:
                    {
                        if (remaining.Length < 3)
                            return false;
                        remaining = remaining.Slice(1);
                        ushort length = BinaryPrimitives.ReadUInt16BigEndian(remaining);
                        remaining = remaining.Slice(2);

                        if (remaining.Length < length)
                            return false;
                        var span = remaining.Slice(0, length);
                        remaining = remaining.Slice(length);
                        return Utf8Reader.TryReadUtf8String(ref span, out result);
                    }
                case EtfTokenType.SmallAtomExt:
                case EtfTokenType.SmallAtomUtf8Ext:
                    {
                        if (remaining.Length < 3)
                            return false;
                        //remaining = remaining.Slice(1);
                        byte length = remaining[1];
                        remaining = remaining.Slice(2);

                        if (remaining.Length < length)
                            return false;
                        var span = remaining.Slice(0, length);
                        remaining = remaining.Slice(length);
                        return Utf8Reader.TryReadUtf8String(ref span, out result);
                    }
                default:
                    return false;
            }
        }
    }
}
