using System;
using System.Buffers;
using System.Runtime.InteropServices;

namespace Voltaic.Serialization.Utf8
{
    public static partial class Utf8Writer
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, char value)
        {
            ReadOnlySpan<char> chars = stackalloc char[] { value };
            var valueBytes = MemoryMarshal.AsBytes(chars);
            if (Encodings.Utf16.ToUtf8Length(valueBytes, out var length) != OperationStatus.Done)
                return false;
            var data = writer.GetSpan(length);
            if (Encodings.Utf16.ToUtf8(valueBytes, data, out _, out _) != OperationStatus.Done)
                return false;
            writer.Advance(length);
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, string value)
        {
            var valueBytes = MemoryMarshal.AsBytes(value.AsSpan());
            if (Encodings.Utf16.ToUtf8Length(valueBytes, out var length) != OperationStatus.Done)
                return false;
            var data = writer.GetSpan(length);
            if (Encodings.Utf16.ToUtf8(valueBytes, data, out _, out _) != OperationStatus.Done)
                return false;
            writer.Advance(length);
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, Utf8String value)
        {
            var srcSpan = value.Bytes;
            var valueBytes = MemoryMarshal.AsBytes(srcSpan);
            var dstSpan = writer.GetSpan(srcSpan.Length);
            srcSpan.CopyTo(dstSpan);
            writer.Advance(srcSpan.Length);
            return true;
        }
    }
}
