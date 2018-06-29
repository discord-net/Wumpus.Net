using System;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfReader
    {
        public static bool TryReadGuid(ref ReadOnlySpan<byte> remaining, out Guid result, char standardFormat)
        {
            throw new NotImplementedException();
        }
    }
}
