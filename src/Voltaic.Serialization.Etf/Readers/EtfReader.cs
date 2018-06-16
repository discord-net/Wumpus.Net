using System;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfReader
    {
        public static TokenType GetTokenType(ref ReadOnlySpan<byte> remaining)
        {
            byte b = remaining[i];
            switch (b)
            {
                case (byte)68:
                    return TokenType.DistributionHeader;
                case (byte)70:
                    return TokenType.NewFloatExt;
                case (byte)77:
                    return TokenType.BitBinaryExt;
                case (byte)80:
                    return TokenType.Compressed;
                case (byte)82:
                    return TokenType.AtomCacheRef;
                case (byte)97:
                    return TokenType.SmallIntegerExt;
                case (byte)98:
                    return TokenType.IntegerExt;
                case (byte)99:
                    return TokenType.FloatExt;
                case (byte)100:
                    return TokenType.AtomExt;
                case (byte)101:
                    return TokenType.ReferenceExt;
                case (byte)102:
                    return TokenType.PortExt;
                case (byte)103:
                    return TokenType.PidExt;
                case (byte)104:
                    return TokenType.SmallTupleExt;
                case (byte)105:
                    return TokenType.LargeTupleExt;
                case (byte)106:
                    return TokenType.NilExt;
                case (byte)107:
                    return TokenType.StringExt;
                case (byte)108:
                    return TokenType.ListExt;
                case (byte)109:
                    return TokenType.BinaryExt;
                case (byte)110:
                    return TokenType.SmallBigExt;
                case (byte)111:
                    return TokenType.LargeBigExt;
                case (byte)112:
                    return TokenType.NewFunExt;
                case (byte)113:
                    return TokenType.ExportExt;
                case (byte)114:
                    return TokenType.NewReferenceExt;
                case (byte)115:
                    return TokenType.SmallAtomExt;
                case (byte)116:
                    return TokenType.MapExt;
                case (byte)117:
                    return TokenType.FunExt;
                case (byte)118:
                    return TokenType.AtomUtf8Ext;
                case (byte)119:
                    return TokenType.SmallAtomUtf8Ext;
                default:
                    throw new SerializationException($"Unexpected type id ({b})");
            }
        }
    }
}
