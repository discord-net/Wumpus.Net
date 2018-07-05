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
            // TODO: Untested, does Discord have any endpoints that accept floats?
            if (standardFormat.IsDefault)
            {
                writer.Push((byte)EtfTokenType.NewFloat);

                // Swap endian
                Span<double> src = stackalloc double[] { value };
                var srcBytes = MemoryMarshal.AsBytes(src);
                var dst = writer.GetSpan(8);
                
                dst[0] = srcBytes[7];
                dst[1] = srcBytes[6];
                dst[2] = srcBytes[5];
                dst[3] = srcBytes[4];
                dst[4] = srcBytes[3];
                dst[5] = srcBytes[2];
                dst[6] = srcBytes[1];
                dst[7] = srcBytes[0];
                writer.Advance(8);
            }
            else
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
            }
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, double value, StandardFormat standardFormat)
        {
            // TODO: Untested, does Discord have any endpoints that accept floats?
            if (standardFormat.IsDefault)
            {
                writer.Push((byte)EtfTokenType.NewFloat);

                // Swap endian
                Span<double> src = stackalloc double[] { value };
                var srcBytes = MemoryMarshal.AsBytes(src);
                var dst = writer.GetSpan(8);

                dst[0] = srcBytes[7];
                dst[1] = srcBytes[6];
                dst[2] = srcBytes[5];
                dst[3] = srcBytes[4];
                dst[4] = srcBytes[3];
                dst[5] = srcBytes[2];
                dst[6] = srcBytes[1];
                dst[7] = srcBytes[0];
                writer.Advance(8);
            }
            else
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
            int length = writer.Length - start - 5;
            writer.Array[start + 1] = (byte)(length >> 24);
            writer.Array[start + 2] = (byte)(length >> 16);
            writer.Array[start + 3] = (byte)(length >> 8);
            writer.Array[start + 4] = (byte)length;
            return true;
        }
    }
}
