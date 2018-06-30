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
                writer.Push((byte)EtfTokenType.NewFloatExt);
                MemoryMarshal.AsBytes(arr).CopyTo(writer.GetSpan(8));
                writer.Advance(8);
            }
            else
            {
                writer.Push((byte)EtfTokenType.StringExt);
                writer.Advance(2);
                int start = writer.Length;
                if (!Utf8Writer.TryWrite(ref writer, value, standardFormat))
                    return false;
                int length = writer.Length - start;
                if (length > ushort.MaxValue)
                    return false;
                writer.Array[start - 2] = (byte)(length >> 8);
                writer.Array[start - 1] = (byte)(length & 0xFF);
            }
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, double value, StandardFormat standardFormat)
        {
            if (standardFormat.IsDefault)
            {
                Span<float> arr = stackalloc float[] { (float)value };
                writer.Push((byte)EtfTokenType.NewFloatExt);
                MemoryMarshal.AsBytes(arr).CopyTo(writer.GetSpan(8));
                writer.Advance(8);
            }
            else
            {
                writer.Push((byte)EtfTokenType.StringExt);
                writer.Advance(2);
                int start = writer.Length;
                if (!Utf8Writer.TryWrite(ref writer, value, standardFormat))
                    return false;
                int length = writer.Length - start;
                if (length > ushort.MaxValue)
                    return false;
                writer.Array[start - 2] = (byte)(length >> 8);
                writer.Array[start - 1] = (byte)(length & 0xFF);
            }
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, decimal value, StandardFormat standardFormat)
        {
            writer.Push((byte)EtfTokenType.StringExt);
            writer.Advance(2);
            int start = writer.Length;
            if (!Utf8Writer.TryWrite(ref writer, value, standardFormat))
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
