using System;
using System.Buffers.Binary;
using Voltaic.Serialization.Etf;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfReader
    {
        private enum StringOperationStatus
        {
            Failed,
            Builder,
            Direct
        }

        public static bool TryReadChar(ref ReadOnlySpan<byte> remaining, out char result)
        {
            result = default;            

            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.StringExt:
                    {
                        if (remaining.Length < 2)
                            return false;
                        ushort length = BinaryPrimitives.ReadUInt16BigEndian(remaining);
                        remaining = remaining.Slice(2);

                        if (remaining.Length < length)
                            return false;
                        var span = remaining.Slice(length);
                        remaining = remaining.Slice(length);
                        return Utf8Reader.TryReadChar(ref span, out result);
                    }
                case EtfTokenType.BinaryExt:
                    {
                        if (remaining.Length < 4)
                            return false;
                        uint length = BinaryPrimitives.ReadUInt32BigEndian(remaining);
                        remaining = remaining.Slice(4);

                        if (remaining.Length < length)
                            return false;
                        var span = remaining.Slice((int)length);
                        remaining = remaining.Slice((int)length);
                        return Utf8Reader.TryReadChar(ref span, out result);
                    }
                default:
                    return false;
            }
        }

        public static bool TryReadString(ref ReadOnlySpan<byte> remaining, out string result)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.StringExt:
                    {
                        if (remaining.Length < 2)
                            return false;
                        ushort length = BinaryPrimitives.ReadUInt16BigEndian(remaining);
                        remaining = remaining.Slice(2);

                        if (remaining.Length < length)
                            return false;
                        var span = remaining.Slice(length);
                        remaining = remaining.Slice(length);
                        return Utf8Reader.TryReadString(ref span, out result);
                    }
                case EtfTokenType.BinaryExt:
                    {
                        if (remaining.Length < 4)
                            return false;
                        uint length = BinaryPrimitives.ReadUInt32BigEndian(remaining);
                        remaining = remaining.Slice(4);

                        if (remaining.Length < length)
                            return false;
                        var span = remaining.Slice((int)length);
                        remaining = remaining.Slice((int)length);
                        return Utf8Reader.TryReadString(ref span, out result);
                    }
                default:
                    return false;
            }
        }

        public static bool TryReadUtf8String(ref ReadOnlySpan<byte> remaining, out Utf8String result)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.StringExt:
                    {
                        if (remaining.Length < 2)
                            return false;
                        ushort length = BinaryPrimitives.ReadUInt16BigEndian(remaining);
                        remaining = remaining.Slice(2);

                        if (remaining.Length < length)
                            return false;
                        var span = remaining.Slice(length);
                        remaining = remaining.Slice(length);
                        return Utf8Reader.TryReadUtf8String(ref span, out result);
                    }
                case EtfTokenType.BinaryExt:
                    {
                        if (remaining.Length < 4)
                            return false;
                        uint length = BinaryPrimitives.ReadUInt32BigEndian(remaining);
                        remaining = remaining.Slice(4);

                        if (remaining.Length < length)
                            return false;
                        var span = remaining.Slice((int)length);
                        remaining = remaining.Slice((int)length);
                        return Utf8Reader.TryReadUtf8String(ref span, out result);
                    }
                default:
                    return false;
            }
        }
    }
}
