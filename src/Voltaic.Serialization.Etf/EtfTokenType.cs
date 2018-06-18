namespace Voltaic.Serialization.Etf
{
    public enum EtfTokenType : byte
    {
        None = 0,
        DistributionHeader,
        NewFloatExt,
        BitBinaryExt,
        Compressed,
        AtomCacheRef,
        SmallIntegerExt,
        IntegerExt,
        FloatExt,
        AtomExt,
        ReferenceExt,
        PortExt,
        PidExt,
        SmallTupleExt,
        LargeTupleExt,
        NilExt,
        StringExt,
        ListExt,
        BinaryExt,
        SmallBigExt,
        LargeBigExt,
        NewFunExt,
        ExportExt,
        NewReferenceExt,
        SmallAtomExt,
        MapExt,
        FunExt,
        AtomUtf8Ext,
        SmallAtomUtf8Ext
    }
}
