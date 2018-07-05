using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Runtime.InteropServices;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, char value)
        {
            int start = writer.Length;
            writer.Push((byte)EtfTokenType.Binary);
            writer.Advance(4);
            if (!Utf8Writer.TryWrite(ref writer, value))
                return false;
            int length = writer.Length - start - 5;
            writer.Array[start + 1] = (byte)(length >> 24);
            writer.Array[start + 2] = (byte)(length >> 16);
            writer.Array[start + 3] = (byte)(length >> 8);
            writer.Array[start + 4] = (byte)length;
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, string value)
        {
            int start = writer.Length;
            writer.Push((byte)EtfTokenType.Binary);
            writer.Advance(4);
            if (!Utf8Writer.TryWrite(ref writer, value))
                return false;
            int length = writer.Length - start - 5;
            writer.Array[start + 1] = (byte)(length >> 24);
            writer.Array[start + 2] = (byte)(length >> 16);
            writer.Array[start + 3] = (byte)(length >> 8);
            writer.Array[start + 4] = (byte)length;
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, Utf8String value)
            => TryWriteUtf8Bytes(ref writer, value.Bytes);
        public static bool TryWrite(ref ResizableMemory<byte> writer, Utf8Span value)
            => TryWriteUtf8Bytes(ref writer, value.Bytes);

        public static bool TryWriteUtf8Bytes(ref ResizableMemory<byte> writer, ReadOnlyMemory<byte> value)
            => TryWriteUtf8Bytes(ref writer, value.Span);
        public static bool TryWriteUtf8Bytes(ref ResizableMemory<byte> writer, ReadOnlySpan<byte> value)
        {
            if (value.Length > ushort.MaxValue)
                return false;

            writer.Push((byte)EtfTokenType.Binary);
            BinaryPrimitives.WriteUInt32BigEndian(writer.GetSpan(4), (uint)value.Length);
            writer.Advance(4);
            if (!Utf8Writer.TryWriteString(ref writer, value))
                return false;
            return true;
        }

        public static bool TryWriteUtf16Key(ref ResizableMemory<byte> writer, string value)
            => TryWrite(ref writer, value);

        public static bool TryWriteUtf8Key(ref ResizableMemory<byte> writer, Utf8String value)
            => TryWriteUtf8Bytes(ref writer, value.Bytes);
        public static bool TryWriteUtf8Key(ref ResizableMemory<byte> writer, Utf8Span value)
            => TryWriteUtf8Bytes(ref writer, value.Bytes);

        public static bool TryWriteUtf8Key(ref ResizableMemory<byte> writer, ReadOnlyMemory<byte> value)
            => TryWriteUtf8Bytes(ref writer, value.Span);
        public static bool TryWriteUtf8Key(ref ResizableMemory<byte> writer, ReadOnlySpan<byte> value)
            => TryWriteUtf8Bytes(ref writer, value);
    }
}
