using System;
using System.Buffers.Binary;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfReader
    {
        public static bool TryReadBoolean(ref ReadOnlySpan<byte> remaining, out bool result, char standardFormat)
        {
            result = default;

            if (standardFormat != '\0')
            {
                if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                    return false;
                return Utf8Reader.TryReadBoolean(ref bytes, out result, standardFormat);
            }

            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.SmallAtom:
                case EtfTokenType.SmallAtomUtf8:
                    {
                        if (remaining.Length < 2)
                            return false;
                        //remaining = remaining.Slice(1);
                        byte length = remaining[1];
                        //remaining = remaining.Slice(1);
                        switch (length)
                        {
                            case 4:
                                remaining = remaining.Slice(6);
                                result = true;
                                return true;
                            case 5:
                                remaining = remaining.Slice(7);
                                result = false;
                                return true;
                            default:
                                return false;
                        }
                    }
                case EtfTokenType.Atom:
                case EtfTokenType.AtomUtf8:
                    {
                        if (remaining.Length < 3)
                            return false;
                        remaining = remaining.Slice(1);
                        ushort length = BinaryPrimitives.ReadUInt16BigEndian(remaining);
                        //remaining = remaining.Slice(2);
                        switch (length)
                        {
                            case 4:
                                remaining = remaining.Slice(6);
                                result = true;
                                return true;
                            case 5:
                                remaining = remaining.Slice(7);
                                result = false;
                                return true;
                            default:
                                return false;
                        }
                    }
                default:
                    return false;
            }
        }
    }
}
