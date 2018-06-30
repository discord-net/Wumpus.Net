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
        public static bool TryWrite(ref ResizableMemory<byte> writer, Utf8Span value)
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
    }
}
