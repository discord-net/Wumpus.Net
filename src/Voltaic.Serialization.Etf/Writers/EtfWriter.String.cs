using System;
using System.Buffers;
using System.Runtime.InteropServices;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, char value)
        {
            throw new NotImplementedException();
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, string value)
        {
            throw new NotImplementedException();
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, Utf8String value)
        {
            throw new NotImplementedException();
        }
        public static bool TryWrite(ref ResizableMemory<byte> writer, Utf8Span value)
        {
            throw new NotImplementedException();
        }

        public static bool TryWriteUtf8(ref ResizableMemory<byte> writer, ReadOnlyMemory<byte> value)
            => TryWriteUtf8(ref writer, value.Span);
        public static bool TryWriteUtf8(ref ResizableMemory<byte> writer, ReadOnlySpan<byte> value)
        {
            throw new NotImplementedException();
        }
    }
}
