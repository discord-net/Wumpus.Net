using System;
using System.Buffers;
using System.Runtime.InteropServices;

namespace Voltaic.Serialization.Utf8
{
    public static partial class Utf8Writer
    {
        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, char value)
        {
            Span<char> chars = stackalloc char[] { value };
            var valueBytes = MemoryMarshal.AsBytes(chars);
            if (Encodings.Utf16.ToUtf8Length(valueBytes, out var length) != OperationStatus.Done)
                return false;
            var data = writer.GetSpan(length);
            if (Encodings.Utf16.ToUtf8(valueBytes, data, out _, out _) != OperationStatus.Done)
                return false;
            writer.Write(data.Slice(0, length));
            return true;
        }

        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, string value)
        {
            var valueBytes = MemoryMarshal.AsBytes(value.AsSpan());
            if (Encodings.Utf16.ToUtf8Length(valueBytes, out var length) != OperationStatus.Done)
                return false;
            var data = writer.GetSpan(length);
            if (Encodings.Utf16.ToUtf8(valueBytes, data, out _, out _) != OperationStatus.Done)
                return false;
            writer.Write(data.Slice(0, length));
            return true;
        }
    }
}
