using System;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, DateTime value)
        {
            throw new NotImplementedException();
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, DateTimeOffset value)
        {
            throw new NotImplementedException();
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, TimeSpan value)
        {
            throw new NotImplementedException();
        }
    }
}
