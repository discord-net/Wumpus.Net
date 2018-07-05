using System;
using System.Buffers.Binary;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfReader
    {
        public static bool TryReadUInt8(ref ReadOnlySpan<byte> remaining, out byte result, char standardFormat)
        {
            result = default;

            if (standardFormat != '\0')
            {
                if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                    return false;
                return Utf8Reader.TryReadUInt8(ref bytes, out result, standardFormat);
            }

            result = default;
            if (!TryReadUInt64(ref remaining, out ulong longResult, standardFormat))
                return false;
            if (longResult > byte.MaxValue)
                return false;
            result = (byte)longResult;
            return true;
        }

        public static bool TryReadUInt16(ref ReadOnlySpan<byte> remaining, out ushort result, char standardFormat)
        {
            result = default;

            if (standardFormat != '\0')
            {
                if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                    return false;
                return Utf8Reader.TryReadUInt16(ref bytes, out result, standardFormat);
            }

            result = default;
            if (!TryReadUInt64(ref remaining, out ulong longResult, standardFormat))
                return false;
            if (longResult > ushort.MaxValue)
                return false;
            result = (ushort)longResult;
            return true;
        }

        public static bool TryReadUInt32(ref ReadOnlySpan<byte> remaining, out uint result, char standardFormat)
        {
            result = default;

            if (standardFormat != '\0')
            {
                if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                    return false;
                return Utf8Reader.TryReadUInt32(ref bytes, out result, standardFormat);
            }

            result = default;
            if (!TryReadUInt64(ref remaining, out ulong longResult, standardFormat))
                return false;
            if (longResult > uint.MaxValue)
                return false;
            result = (uint)longResult;
            return true;
        }

        public static bool TryReadUInt64(ref ReadOnlySpan<byte> remaining, out ulong result, char standardFormat)
        {
            result = default;

            if (standardFormat != '\0')
            {
                if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                    return false;
                return Utf8Reader.TryReadUInt64(ref bytes, out result, standardFormat);
            }

            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.SmallInteger:
                    {
                        //remaining = remaining.Slice(1);
                        result = remaining[1];
                        remaining = remaining.Slice(2);
                        return true;
                    }
                case EtfTokenType.Integer:
                    {
                        remaining = remaining.Slice(1);
                        int signedResult = BinaryPrimitives.ReadInt32BigEndian(remaining);
                        if (signedResult < 0)
                            return false;
                        result = (ulong)signedResult;
                        remaining = remaining.Slice(4);
                        return true;
                    }
                case EtfTokenType.SmallBig:
                    {
                        //remaining = remaining.Slice(1);
                        byte bytes = remaining[1];
                        bool isPositive = remaining[2] == 0;
                        remaining = remaining.Slice(3);
                        return TryReadUnsignedBigNumber(bytes, isPositive, ref remaining, out result);
                    }
                case EtfTokenType.LargeBig:
                    {
                        remaining = remaining.Slice(1);
                        if (!BinaryPrimitives.TryReadUInt32BigEndian(remaining, out uint bytes))
                            return false;
                        if (bytes > int.MaxValue)
                            return false; // TODO: Spans dont allow uint accessors
                        bool isPositive = remaining[4] == 0;
                        remaining = remaining.Slice(5);
                        return TryReadUnsignedBigNumber((int)bytes, isPositive, ref remaining, out result);
                    }
                default:
                    return false;
            }
        }

        private static bool TryReadUnsignedBigNumber(int bytes, bool isPositive, ref ReadOnlySpan<byte> remaining, out ulong result)
        {
            result = default;
            if (!isPositive)
                return false;
            switch (bytes)
            {
                case 1:
                    {
                        result = remaining[0];
                        remaining = remaining.Slice(1);
                        return true;
                    }
                case 2:
                    {
                        result = BinaryPrimitives.ReadUInt16LittleEndian(remaining);
                        remaining = remaining.Slice(2);
                        return true;
                    }
                case 4:
                    {
                        result = BinaryPrimitives.ReadUInt32LittleEndian(remaining);
                        remaining = remaining.Slice(4);
                        return true;
                    }
                case 8:
                    {
                        result = BinaryPrimitives.ReadUInt64LittleEndian(remaining);
                        remaining = remaining.Slice(8);
                        return true;
                    }
                default:
                    {
                        if (bytes > 8)
                            return false; // TODO: Support BigNumber
                        ulong multiplier = 1;
                        for (int i = 0; i < bytes; i++, multiplier *= 256)
                            result += remaining[i] * multiplier;
                        remaining = remaining.Slice(bytes);
                        return true;
                    }
            }
        }
    }
}
