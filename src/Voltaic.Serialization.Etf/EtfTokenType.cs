namespace Voltaic.Serialization.Etf
{
    public enum EtfTokenType : byte
    {
        None = 0,
        DistributionHeader = 68,
        NewFloatExt = 70,
        BitBinaryExt = 77,
        Compressed = 80,
        AtomCacheRef = 82,
        SmallIntegerExt = 97,
        IntegerExt = 98,
        FloatExt = 99,
        AtomExt = 100,
        ReferenceExt = 101,
        PortExt = 102,
        PidExt = 103,
        SmallTupleExt = 104,
        LargeTupleExt = 105,
        NilExt = 106,
        StringExt = 107,
        ListExt = 108,
        BinaryExt = 109,
        SmallBigExt = 110,
        LargeBigExt = 111,
        NewFunExt = 112,
        ExportExt = 113,
        NewReferenceExt = 114,
        SmallAtomExt = 115,
        MapExt = 116,
        FunExt = 117,
        AtomUtf8Ext = 118,
        SmallAtomUtf8Ext = 119
    }
}
