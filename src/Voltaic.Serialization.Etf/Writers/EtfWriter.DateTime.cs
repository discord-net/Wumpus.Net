using System;
using System.Buffers;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, DateTime value, StandardFormat standardFormat)
        {
            int start = writer.Length;
            writer.Push((byte)EtfTokenType.Binary);
            writer.Advance(4);
            if (!Utf8Writer.TryWrite(ref writer, value, standardFormat))
                return false;
            int length = writer.Length - start - 5;
            writer.Array[start + 1] = (byte)(length >> 24);
            writer.Array[start + 2] = (byte)(length >> 16);
            writer.Array[start + 3] = (byte)(length >> 8);
            writer.Array[start + 4] = (byte)length;
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, DateTimeOffset value, StandardFormat standardFormat)
        {
            int start = writer.Length;
            writer.Push((byte)EtfTokenType.Binary);
            writer.Advance(4);
            if (!Utf8Writer.TryWrite(ref writer, value, standardFormat))
                return false;
            int length = writer.Length - start - 5;
            writer.Array[start + 1] = (byte)(length >> 24);
            writer.Array[start + 2] = (byte)(length >> 16);
            writer.Array[start + 3] = (byte)(length >> 8);
            writer.Array[start + 4] = (byte)length;
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, TimeSpan value, StandardFormat standardFormat)
        {
            int start = writer.Length;
            writer.Push((byte)EtfTokenType.Binary);
            writer.Advance(4);
            if (!Utf8Writer.TryWrite(ref writer, value, standardFormat))
                return false;
            int length = writer.Length - start - 5;
            writer.Array[start + 1] = (byte)(length >> 24);
            writer.Array[start + 2] = (byte)(length >> 16);
            writer.Array[start + 3] = (byte)(length >> 8);
            writer.Array[start + 4] = (byte)length;
            return true;
        }
    }
}
