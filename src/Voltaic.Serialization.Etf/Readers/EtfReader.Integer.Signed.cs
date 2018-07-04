using System;
using System.Buffers.Binary;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfReader
    {
        public static bool TryReadInt8(ref ReadOnlySpan<byte> remaining, out sbyte result, char standardFormat)
        {
            result = default;

            if (standardFormat != '\0')
            {
                if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                    return false;
                return Utf8Reader.TryReadInt8(ref bytes, out result, standardFormat);
            }

            result = default;
            if (!TryReadInt64(ref remaining, out long longResult, standardFormat))
                return false;
            if (longResult > sbyte.MaxValue || longResult < sbyte.MinValue)
                return false;
            result = (sbyte)longResult;
            return true;
        }

        public static bool TryReadInt16(ref ReadOnlySpan<byte> remaining, out short result, char standardFormat)
        {
            result = default;

            if (standardFormat != '\0')
            {
                if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                    return false;
                return Utf8Reader.TryReadInt16(ref bytes, out result, standardFormat);
            }

            result = default;
            if (!TryReadInt64(ref remaining, out long longResult, standardFormat))
                return false;
            if (longResult > short.MaxValue || longResult < short.MinValue)
                return false;
            result = (short)longResult;
            return true;
        }

        public static bool TryReadInt32(ref ReadOnlySpan<byte> remaining, out int result, char standardFormat)
        {
            result = default;

            if (standardFormat != '\0')
            {
                if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                    return false;
                return Utf8Reader.TryReadInt32(ref bytes, out result, standardFormat);
            }

            result = default;
            if (!TryReadInt64(ref remaining, out long longResult, standardFormat))
                return false;
            if (longResult > int.MaxValue || longResult < int.MinValue)
                return false;
            result = (int)longResult;
            return true;
        }

        public static bool TryReadInt64(ref ReadOnlySpan<byte> remaining, out long result, char standardFormat)
        {
            result = default;

            if (standardFormat != '\0')
            {
                if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                    return false;
                return Utf8Reader.TryReadInt64(ref bytes, out result, standardFormat);
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
                        result = BinaryPrimitives.ReadInt32BigEndian(remaining);
                        remaining = remaining.Slice(4);
                        return true;
                    }
                case EtfTokenType.SmallBig:
                    {
                        //remaining = remaining.Slice(1);
                        byte bytes = remaining[1];
                        bool isPositive = remaining[2] == 0;
                        remaining = remaining.Slice(3);
                        return TryReadSignedBigNumber(bytes, isPositive, ref remaining, out result);
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
                        return TryReadSignedBigNumber((int)bytes, isPositive, ref remaining, out result);
                    }
                default:
                    return false;
            }
        }

        private static bool TryReadSignedBigNumber(int bytes, bool isPositive, ref ReadOnlySpan<byte> remaining, out long result)
        {
            result = default;
            switch (bytes)
            {
                case 1:
                    {
                        result = remaining[0];
                        if (!isPositive)
                            result = -result;
                        remaining = remaining.Slice(1);
                        return true;
                    }
                case 2:
                    {
                        result = BinaryPrimitives.ReadUInt16LittleEndian(remaining);
                        if (!isPositive)
                            result = -result;
                        remaining = remaining.Slice(2);
                        return true;
                    }
                case 4:
                    {
                        result = BinaryPrimitives.ReadUInt32LittleEndian(remaining);
                        if (!isPositive)
                            result = -result;
                        remaining = remaining.Slice(4);
                        return true;
                    }
                case 8:
                    {
                        ulong unsignedResult = BinaryPrimitives.ReadUInt64LittleEndian(remaining);
                        remaining = remaining.Slice(8);

                        if (isPositive)
                        {
                            if (unsignedResult > long.MaxValue)
                                return false;
                            result = (long)unsignedResult;
                        }
                        else
                        {
                            if (unsignedResult > long.MaxValue + 1UL)
                                return false;
                            else if (unsignedResult == long.MaxValue + 1UL)
                                result = long.MinValue;
                            else
                                result = -(long)unsignedResult;
                        }
                        return true;
                    }
                default:
                    {
                        if (bytes > 8)
                            return false; // TODO: Support BigNumber
                        ulong unsignedResult = 0;
                        ulong multiplier = 1;
                        for (int i = 0; i < bytes; i++, multiplier *= 256)
                            unsignedResult += remaining[i] * multiplier;
                        remaining = remaining.Slice(bytes);

                        if (isPositive)
                        {
                            if (unsignedResult > long.MaxValue)
                                return false;
                            result = (long)unsignedResult;
                        }
                        else
                        {
                            if (unsignedResult > long.MaxValue + 1UL)
                                return false;
                            else if (unsignedResult == long.MaxValue + 1UL)
                                result = long.MinValue;
                            else
                                result = -(long)unsignedResult;
                        }
                        return true;
                    }
            }
        }
    }
}
