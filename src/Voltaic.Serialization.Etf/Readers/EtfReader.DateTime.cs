using System;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfReader
    {
        public static bool TryReadDateTime(ref ReadOnlySpan<byte> remaining, out DateTime result)
        {
            throw new NotImplementedException();
        }

        public static bool TryReadDateTimeOffset(ref ReadOnlySpan<byte> remaining, out DateTimeOffset result)
        {
            throw new NotImplementedException();
        }

        public static bool TryReadTimeSpan(ref ReadOnlySpan<byte> remaining, out TimeSpan result)
        {
            throw new NotImplementedException();
        }
    }
}
