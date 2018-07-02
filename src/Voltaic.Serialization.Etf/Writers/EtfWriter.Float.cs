using System;
using System.Buffers;
using System.Runtime.InteropServices;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, float value, StandardFormat standardFormat)
        {
            if (standardFormat.IsDefault)
            {
                Span<float> arr = stackalloc float[] { value };
                writer.Push((byte)EtfTokenType.NewFloat);
                MemoryMarshal.AsBytes(arr).CopyTo(writer.GetSpan(8));
                writer.Advance(8);
            }
            else
            {
                int start = writer.Length;
                writer.Push((byte)EtfTokenType.Binary);
                writer.Advance(4);
                if (!Utf8Writer.TryWrite(ref writer, value, standardFormat))
                    return false;
                int length = writer.Length - start;
                if (length > ushort.MaxValue)
                    return false;
                writer.Array[start + 1] = (byte)(length >> 24);
                writer.Array[start + 2] = (byte)(length >> 16);
                writer.Array[start + 3] = (byte)(length >> 8);
                writer.Array[start + 4] = (byte)length;
            }
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, double value, StandardFormat standardFormat)
        {
            if (standardFormat.IsDefault)
            {
                Span<float> arr = stackalloc float[] { (float)value };
                writer.Push((byte)EtfTokenType.NewFloat);
                MemoryMarshal.AsBytes(arr).CopyTo(writer.GetSpan(8));
                writer.Advance(8);
            }
            else
            {
                int start = writer.Length;
                writer.Push((byte)EtfTokenType.Binary);
                writer.Advance(4);
                if (!Utf8Writer.TryWrite(ref writer, value, standardFormat))
                    return false;
                int length = writer.Length - start;
                if (length > ushort.MaxValue)
                    return false;
                writer.Array[start + 1] = (byte)(length >> 24);
                writer.Array[start + 2] = (byte)(length >> 16);
                writer.Array[start + 3] = (byte)(length >> 8);
                writer.Array[start + 4] = (byte)length;
            }
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, decimal value, StandardFormat standardFormat)
        {
            int start = writer.Length;
            writer.Push((byte)EtfTokenType.Binary);
            writer.Advance(4);
            if (!Utf8Writer.TryWrite(ref writer, value, standardFormat))
                return false;
            int length = writer.Length - start;
            if (length > ushort.MaxValue)
                return false;
            writer.Array[start + 1] = (byte)(length >> 24);
            writer.Array[start + 2] = (byte)(length >> 16);
            writer.Array[start + 3] = (byte)(length >> 8);
            writer.Array[start + 4] = (byte)length;
            return true;
        }
    }
}
