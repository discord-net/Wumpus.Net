using System;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfReader
    {
        public static bool TryReadSingle(ref ReadOnlySpan<byte> remaining, out float result, char standardFormat)
        {
            throw new NotImplementedException();
        }

        public static bool TryReadDouble(ref ReadOnlySpan<byte> remaining, out double result, char standardFormat)
        {
            throw new NotImplementedException();
        }

        public static bool TryReadDecimal(ref ReadOnlySpan<byte> remaining, out decimal result, char standardFormat)
        {
            throw new NotImplementedException();
        }
    }
}
