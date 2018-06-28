using System;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfReader
    {
        public static EtfTokenType GetTokenType(ref ReadOnlySpan<byte> remaining)
        {
            byte b = remaining[0];
            switch (b)
            {
                case 68:
                    return EtfTokenType.DistributionHeader;
                case 70:
                    return EtfTokenType.NewFloatExt;
                case 77:
                    return EtfTokenType.BitBinaryExt;
                case 80:
                    return EtfTokenType.Compressed;
                case 82:
                    return EtfTokenType.AtomCacheRef;
                case 97:
                    return EtfTokenType.SmallIntegerExt;
                case 98:
                    return EtfTokenType.IntegerExt;
                case 99:
                    return EtfTokenType.FloatExt;
                case 100:
                    return EtfTokenType.AtomExt;
                case 101:
                    return EtfTokenType.ReferenceExt;
                case 102:
                    return EtfTokenType.PortExt;
                case 103:
                    return EtfTokenType.PidExt;
                case 104:
                    return EtfTokenType.SmallTupleExt;
                case 105:
                    return EtfTokenType.LargeTupleExt;
                case 106:
                    return EtfTokenType.NilExt;
                case 107:
                    return EtfTokenType.StringExt;
                case 108:
                    return EtfTokenType.ListExt;
                case 109:
                    return EtfTokenType.BinaryExt;
                case 110:
                    return EtfTokenType.SmallBigExt;
                case 111:
                    return EtfTokenType.LargeBigExt;
                case 112:
                    return EtfTokenType.NewFunExt;
                case 113:
                    return EtfTokenType.ExportExt;
                case 114:
                    return EtfTokenType.NewReferenceExt;
                case 115:
                    return EtfTokenType.SmallAtomExt;
                case 116:
                    return EtfTokenType.MapExt;
                case 117:
                    return EtfTokenType.FunExt;
                case 118:
                    return EtfTokenType.AtomUtf8Ext;
                case 119:
                    return EtfTokenType.SmallAtomUtf8Ext;
                default:
                    throw new SerializationException($"Unexpected type id ({b})");
            }
        }

        public static bool Skip(ref ReadOnlySpan<byte> remaining, out ReadOnlySpan<byte> skipped)
        {
            throw new NotImplementedException();
        }
    }
}
