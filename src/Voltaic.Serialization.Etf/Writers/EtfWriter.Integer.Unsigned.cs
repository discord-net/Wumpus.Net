using System.Buffers;
using System.Buffers.Binary;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, byte value, StandardFormat standardFormat)
        {
            if (standardFormat.IsDefault)
            {
                writer.Push((byte)EtfTokenType.SmallInteger);
                writer.Push(value);
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

        public static bool TryWrite(ref ResizableMemory<byte> writer, ushort value, StandardFormat standardFormat)
        {
            if (standardFormat.IsDefault)
            {
                if (value <= byte.MaxValue)
                    return TryWrite(ref writer, (byte)value, standardFormat);

                writer.Push((byte)EtfTokenType.Integer);
                BinaryPrimitives.WriteInt32BigEndian(writer.GetSpan(4), value);
                writer.Advance(4);
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

        public static bool TryWrite(ref ResizableMemory<byte> writer, uint value, StandardFormat standardFormat)
        {
            if (standardFormat.IsDefault)
            {
                if (value <= byte.MaxValue)
                    return TryWrite(ref writer, (byte)value, standardFormat);
                if (value <= int.MaxValue)
                    return TryWrite(ref writer, (int)value, standardFormat);

                writer.Push((byte)EtfTokenType.SmallBig);
                writer.Push(4);
                writer.Push(0);
                BinaryPrimitives.WriteUInt32LittleEndian(writer.GetSpan(8), value);
                writer.Advance(4);
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

        public static bool TryWrite(ref ResizableMemory<byte> writer, ulong value, StandardFormat standardFormat)
        {
            if (standardFormat.IsDefault)
            {
                if (value <= byte.MaxValue)
                    return TryWrite(ref writer, (byte)value, standardFormat);
                if (value <= int.MaxValue)
                    return TryWrite(ref writer, (int)value, standardFormat);

                writer.Push((byte)EtfTokenType.SmallBig);
                writer.Push(8);
                writer.Push(0);
                BinaryPrimitives.WriteUInt64LittleEndian(writer.GetSpan(8), value);
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
    }
}
