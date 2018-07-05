using System;
using System.Buffers.Binary;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfReader
    {
        public static EtfTokenType GetTokenType(ref ReadOnlySpan<byte> remaining)
        {
            if (remaining.Length == 0)
                return EtfTokenType.None;
            byte b = remaining[0];

            switch (b)
            {
                case 68: return EtfTokenType.DistributionHeader;
                case 70: return EtfTokenType.NewFloat;
                case 77: return EtfTokenType.BitBinary;
                case 80: return EtfTokenType.Compressed;
                case 82: return EtfTokenType.AtomCacheRef;
                case 97: return EtfTokenType.SmallInteger;
                case 98: return EtfTokenType.Integer;
                case 99: return EtfTokenType.Float;
                case 100: return EtfTokenType.Atom;
                case 101: return EtfTokenType.Reference;
                case 102: return EtfTokenType.Port;
                case 103: return EtfTokenType.Pid;
                case 104: return EtfTokenType.SmallTuple;
                case 105: return EtfTokenType.LargeTuple;
                case 106: return EtfTokenType.Nil;
                case 107: return EtfTokenType.String;
                case 108: return EtfTokenType.List;
                case 109: return EtfTokenType.Binary;
                case 110: return EtfTokenType.SmallBig;
                case 111: return EtfTokenType.LargeBig;
                case 112: return EtfTokenType.NewFun;
                case 113: return EtfTokenType.Export;
                case 114: return EtfTokenType.NewReference;
                case 115: return EtfTokenType.SmallAtom;
                case 116: return EtfTokenType.Map;
                case 117: return EtfTokenType.Fun;
                case 118: return EtfTokenType.AtomUtf8;
                case 119: return EtfTokenType.SmallAtomUtf8;
                default:
                    throw new SerializationException($"Unexpected type id ({b})");
            }
        }

        public static bool Skip(ref ReadOnlySpan<byte> remaining, out ReadOnlySpan<byte> skipped)
        {
            skipped = default;

            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.Nil: // 1 Byte (1 Header)
                    {
                        skipped = remaining.Slice(0, 1);
                        remaining = remaining.Slice(1);
                        return true;
                    }
                case EtfTokenType.SmallInteger: // 2 Byte (1 Header + 1 Payload)
                    {
                        // remaining = remaining.Slice(1);
                        if (remaining.Length < 2)
                            return false;
                        skipped = remaining.Slice(0, 2);
                        remaining = remaining.Slice(2);
                        return true;
                    }
                case EtfTokenType.Integer: // 5 Byte (1 Header + 4 Payload)
                    {
                        // remaining = remaining.Slice(1);
                        if (remaining.Length < 5)
                            return false;
                        skipped = remaining.Slice(0, 5);
                        remaining = remaining.Slice(5);
                        return true;
                    }
                case EtfTokenType.NewFloat: // 9 Byte (1 Header + 8 Payload)
                    {
                        // remaining = remaining.Slice(1);
                        if (remaining.Length < 9)
                            return false;
                        skipped = remaining.Slice(0, 9);
                        remaining = remaining.Slice(9);
                        return true;
                    }
                case EtfTokenType.Float: // 32 Byte (1 Header + 31 Payload)
                    {
                        // remaining = remaining.Slice(1);
                        if (remaining.Length < 32)
                            return false;
                        skipped = remaining.Slice(0, 32);
                        remaining = remaining.Slice(32);
                        return true;
                    }

                case EtfTokenType.SmallAtom: // Variable (1 Header + 1 Length)
                case EtfTokenType.SmallAtomUtf8:
                    {
                        // remaining = remaining.Slice(1);
                        if (remaining.Length < 1)
                            return false;

                        byte len = remaining[1];
                        if (remaining.Length < len + 1)
                            return false;

                        skipped = remaining.Slice(0, len + 2);
                        remaining = remaining.Slice(len + 2);
                        return true;
                    }

                case EtfTokenType.SmallBig: // Variable (1 Header + 1 Length + 1 Payload)
                    {
                        // remaining = remaining.Slice(1);
                        if (remaining.Length < 2)
                            return false;

                        byte len = remaining[1];
                        if (remaining.Length < len + 3)
                            return false;

                        skipped = remaining.Slice(0, len + 3);
                        remaining = remaining.Slice(len + 3);
                        return true;
                    }

                case EtfTokenType.Atom: // Variable (1 Header + 2 Length)
                case EtfTokenType.AtomUtf8:
                case EtfTokenType.String:
                    {
                        if (remaining.Length < 3)
                            return false;

                        var initial = remaining;
                        remaining = remaining.Slice(1);
                        ushort len = BinaryPrimitives.ReadUInt16BigEndian(remaining);

                        if (remaining.Length < len + 2)
                            return false;
                        skipped = initial.Slice(0, len + 3);
                        remaining = remaining.Slice(len + 2);
                        return true;
                    }

                case EtfTokenType.Binary: // Variable (1 Header + 4 Length)
                    {
                        if (remaining.Length < 5)
                            return false;

                        var initial = remaining;
                        remaining = remaining.Slice(1);
                        uint len = BinaryPrimitives.ReadUInt32BigEndian(remaining);

                        if (remaining.Length < len + 4L)
                            return false;
                        skipped = initial.Slice(0, (int)len + 5);
                        remaining = remaining.Slice((int)len + 4);
                        return true;
                    }

                case EtfTokenType.LargeBig: // Variable (1 Header + 4 Length + 1 Payload)
                case EtfTokenType.BitBinary:
                    {
                        if (remaining.Length < 6)
                            return false;

                        var initial = remaining;
                        remaining = remaining.Slice(1);
                        uint len = BinaryPrimitives.ReadUInt32BigEndian(remaining);
                        //remaining = remaining.Slice(1);

                        if (remaining.Length < len + 5L)
                            return false;
                        skipped = initial.Slice(0, (int)len + 6);
                        remaining = remaining.Slice((int)len + 5);
                        return true;
                    }

                case EtfTokenType.SmallTuple: // Variable (1 Header + 1 Element Count)
                    {
                        // remaining = remaining.Slice(1);
                        if (remaining.Length < 2)
                            return false;

                        var initial = remaining;
                        byte count = remaining[1];
                        remaining = remaining.Slice(2);
                        int len = 0;

                        for (int i = 0; i < count; i++)
                        {
                            if (!Skip(ref remaining, out var skippedItem))
                                return false;
                            len += skippedItem.Length;
                        }
                        skipped = initial.Slice(0, len + 2);

                        return true;
                    }
                case EtfTokenType.LargeTuple: // Variable (1 Header + 4 Element Count)
                    {
                        if (remaining.Length < 5)
                            return false;

                        var initial = remaining;
                        remaining = remaining.Slice(1);
                        uint count = BinaryPrimitives.ReadUInt32BigEndian(remaining);
                        remaining = remaining.Slice(4);
                        int len = 0;

                        for (int i = 0; i < count; i++)
                        {
                            if (!Skip(ref remaining, out var skippedItem))
                                return false;
                            len += skippedItem.Length;
                        }
                        skipped = initial.Slice(0, len + 5);

                        return true;
                    }

                case EtfTokenType.Map: // Variable (1 Header + 4 Key+Element Count)
                    {
                        if (remaining.Length < 5)
                            return false;

                        var initial = remaining;
                        remaining = remaining.Slice(1);
                        uint count = BinaryPrimitives.ReadUInt32BigEndian(remaining) * 2;
                        remaining = remaining.Slice(4);
                        int len = 0;

                        for (int i = 0; i < count; i++)
                        {
                            if (!Skip(ref remaining, out var skippedItem))
                                return false;
                            len += skippedItem.Length;
                        }
                        skipped = initial.Slice(0, len + 5);

                        return true;
                    }

                case EtfTokenType.List: // Variable (1 Header + 4 Element Count + Tail Element)
                    {
                        if (remaining.Length < 5)
                            return false;

                        var initial = remaining;
                        remaining = remaining.Slice(1);
                        uint count = BinaryPrimitives.ReadUInt32BigEndian(remaining) + 1;
                        remaining = remaining.Slice(4);
                        int len = 0;

                        for (int i = 0; i < count; i++)
                        {
                            if (!Skip(ref remaining, out var skippedItem))
                                return false;
                            len += skippedItem.Length;
                        }
                        skipped = initial.Slice(0, len + 5);

                        return true;
                    }
                default:
                    return false;
            }
        }

        // Safe as it is non-destructive on failures. There is no easy way to know if something is a null without reading the entire token
        // TODO: Do we want all failures to be safe? This should probably be universal
        public static bool TryReadNullSafe(ref ReadOnlySpan<byte> remaining)
        {
            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.Nil:
                    {
                        if (remaining.Length < 1)
                            return false;
                        remaining = remaining.Slice(1);
                        return true;
                    }
                case EtfTokenType.Atom:
                case EtfTokenType.AtomUtf8:
                    {
                        if (remaining.Length < 6)
                            return false;
                        ushort len = BinaryPrimitives.ReadUInt16BigEndian(remaining.Slice(1));
                        if (len != 3 || remaining[3] != 'n' || remaining[4] != 'i' || remaining[5] != 'l')
                            return false;
                        remaining = remaining.Slice(6);
                        return true;
                    }
                case EtfTokenType.SmallAtom:
                case EtfTokenType.SmallAtomUtf8:
                    {
                        if (remaining.Length < 5)
                            return false;
                        byte len = remaining[1];
                        if (len != 3 || remaining[2] != 'n' || remaining[3] != 'i' || remaining[4] != 'l')
                            return false;
                        remaining = remaining.Slice(5);
                        return true;
                    }
                default:
                    return false;
            }
        }
    }
}
