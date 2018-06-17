using System;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfReader
    {
        public static TokenType GetTokenType(ref ReadOnlySpan<byte> remaining)
        {
            byte b = remaining[0];
            switch (b)
            {
                case 68:
                    return TokenType.DistributionHeader;
                case 70:
                    return TokenType.NewFloatExt;
                case 77:
                    return TokenType.BitBinaryExt;
                case 80:
                    return TokenType.Compressed;
                case 82:
                    return TokenType.AtomCacheRef;
                case 97:
                    return TokenType.SmallIntegerExt;
                case 98:
                    return TokenType.IntegerExt;
                case 99:
                    return TokenType.FloatExt;
                case 100:
                    return TokenType.AtomExt;
                case 101:
                    return TokenType.ReferenceExt;
                case 102:
                    return TokenType.PortExt;
                case 103:
                    return TokenType.PidExt;
                case 104:
                    return TokenType.SmallTupleExt;
                case 105:
                    return TokenType.LargeTupleExt;
                case 106:
                    return TokenType.NilExt;
                case 107:
                    return TokenType.StringExt;
                case 108:
                    return TokenType.ListExt;
                case 109:
                    return TokenType.BinaryExt;
                case 110:
                    return TokenType.SmallBigExt;
                case 111:
                    return TokenType.LargeBigExt;
                case 112:
                    return TokenType.NewFunExt;
                case 113:
                    return TokenType.ExportExt;
                case 114:
                    return TokenType.NewReferenceExt;
                case 115:
                    return TokenType.SmallAtomExt;
                case 116:
                    return TokenType.MapExt;
                case 117:
                    return TokenType.FunExt;
                case 118:
                    return TokenType.AtomUtf8Ext;
                case 119:
                    return TokenType.SmallAtomUtf8Ext;
                default:
                    throw new SerializationException($"Unexpected type id ({b})");
            }
        }
    }
}
