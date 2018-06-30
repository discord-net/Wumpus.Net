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
            => TryWriteString(ref writer, value.Bytes);
        public static bool TryWrite(ref ResizableMemory<byte> writer, Utf8Span value)
            => TryWriteString(ref writer, value.Bytes);

        public static bool TryWriteString(ref ResizableMemory<byte> writer, ReadOnlyMemory<byte> value)
            => TryWriteString(ref writer, value.Span);
        public static bool TryWriteString(ref ResizableMemory<byte> writer, ReadOnlySpan<byte> value)
        {
            var dstSpan = writer.GetSpan(value.Length);
            value.CopyTo(dstSpan);
            writer.Advance(value.Length);
            return true;
        }
    }
}
