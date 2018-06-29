using System;
using System.Buffers.Binary;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfReader
    {
        public static bool TryReadUInt8(ref ReadOnlySpan<byte> remaining, out byte result)
        {
            result = default;
            if (!TryReadUInt64(ref remaining, out ulong longResult))
                return false;
            if (longResult > byte.MaxValue)
                return false;
            result = (byte)longResult;
            return true;
        }

        public static bool TryReadUInt16(ref ReadOnlySpan<byte> remaining, out ushort result)
        {
            result = default;
            if (!TryReadUInt64(ref remaining, out ulong longResult))
                return false;
            if (longResult > ushort.MaxValue)
                return false;
            result = (ushort)longResult;
            return true;
        }

        public static bool TryReadUInt32(ref ReadOnlySpan<byte> remaining, out uint result)
        {
            result = default;
            if (!TryReadUInt64(ref remaining, out ulong longResult))
                return false;
            if (longResult > uint.MaxValue)
                return false;
            result = (uint)longResult;
            return true;
        }

        public static bool TryReadUInt64(ref ReadOnlySpan<byte> remaining, out ulong result)
        {
            result = default;
            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.SmallIntegerExt:
                    {
                        //remaining = remaining.Slice(1);
                        result = remaining[1];
                        remaining = remaining.Slice(2);
                        return true;
                    }
                case EtfTokenType.IntegerExt:
                    {
                        remaining = remaining.Slice(1);
                        int signedResult = BinaryPrimitives.ReadInt32BigEndian(remaining);
                        if (signedResult < 0)
                            return false;
                        result = (ulong)signedResult;
                        remaining = remaining.Slice(4);
                        return true;
                    }
                case EtfTokenType.SmallBigExt:
                    {
                        //remaining = remaining.Slice(1);
                        byte bytes = remaining[1];
                        bool isPositive = remaining[2] == 0;
                        remaining = remaining.Slice(2);
                        return TryReadUnsignedBigNumber(bytes, isPositive, ref remaining, out result);
                    }
                case EtfTokenType.LargeBigExt:
                    {
                        remaining = remaining.Slice(1);
                        if (!BinaryPrimitives.TryReadUInt32BigEndian(remaining, out uint bytes))
                            return false;
                        if (bytes > int.MaxValue)
                            return false; // TODO: Spans dont allow uint accessors
                        bool isPositive = remaining[2] == 0;
                        remaining = remaining.Slice(6);
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
                        result = remaining[3];
                        remaining = remaining.Slice(3);
                        return true;
                    }
                case 2:
                    {
                        result = BinaryPrimitives.ReadUInt16BigEndian(remaining);
                        remaining = remaining.Slice(2);
                        return true;
                    }
                case 4:
                    {
                        result = BinaryPrimitives.ReadUInt32BigEndian(remaining);
                        remaining = remaining.Slice(2);
                        return true;
                    }
                case 8:
                    {
                        result = BinaryPrimitives.ReadUInt64BigEndian(remaining);
                        remaining = remaining.Slice(2);
                        return true;
                    }
                default:
                    {
                        if (bytes > 8)
                            return false; // TODO: Support BigNumber
                        ulong multiplier = 1;
                        for (int i = 0; i < bytes; i++, multiplier *= 256)
                            result += remaining[i] * multiplier;
                        return true;
                    }
            }
        }
    }
}
