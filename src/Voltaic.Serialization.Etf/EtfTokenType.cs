namespace Voltaic.Serialization.Etf
{
    public enum EtfTokenType : byte
    {
        None = 0,
        DistributionHeader = 68,
        NewFloat = 70,
        BitBinary = 77,
        Compressed = 80,
        AtomCacheRef = 82,
        SmallInteger = 97,
        Integer = 98,
        Float = 99,
        Atom = 100,
        Reference = 101,
        Port = 102,
        Pid = 103,
        SmallTuple = 104,
        LargeTuple = 105,
        Nil = 106,
        String = 107,
        List = 108,
        Binary = 109,
        SmallBig = 110,
        LargeBig = 111,
        NewFun = 112,
        Export = 113,
        NewReference = 114,
        SmallAtom = 115,
        Map = 116,
        Fun = 117,
        AtomUtf8 = 118,
        SmallAtomUtf8 = 119
    }
}
