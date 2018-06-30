using System;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, char value)
        {
            writer.Push((byte)EtfTokenType.StringExt);
            writer.Advance(2);
            int start = writer.Length;
            if (!Utf8Writer.TryWrite(ref writer, value))
                return false;
            int length = writer.Length - start;
            if (length > ushort.MaxValue)
                return false;
            writer.Array[start - 2] = (byte)(length >> 8);
            writer.Array[start - 1] = (byte)(length & 0xFF);
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, string value)
        {
            writer.Push((byte)EtfTokenType.StringExt);
            writer.Advance(2);
            int start = writer.Length;
            if (!Utf8Writer.TryWrite(ref writer, value))
                return false;
            int length = writer.Length - start;
            if (length > ushort.MaxValue)
                return false;
            writer.Array[start - 2] = (byte)(length >> 8);
            writer.Array[start - 1] = (byte)(length & 0xFF);
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
            writer.Push((byte)EtfTokenType.StringExt);
            writer.Advance(2);
            int start = writer.Length;
            if (!Utf8Writer.TryWriteString(ref writer, value))
                return false;
            int length = writer.Length - start;
            if (length > ushort.MaxValue)
                return false;
            writer.Array[start - 2] = (byte)(length >> 8);
            writer.Array[start - 1] = (byte)(length & 0xFF);
            return true;
        }
    }
}
