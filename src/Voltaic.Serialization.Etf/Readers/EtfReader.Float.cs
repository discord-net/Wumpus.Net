using System;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfReader
    {
        public static bool TryReadSingle(ref ReadOnlySpan<byte> remaining, out float result)
        {
            throw new NotImplementedException();
        }

        public static bool TryReadDouble(ref ReadOnlySpan<byte> remaining, out double result)
        {
            throw new NotImplementedException();
        }

        public static bool TryReadDecimal(ref ReadOnlySpan<byte> remaining, out decimal result)
        {
            throw new NotImplementedException();
        }
    }
}
