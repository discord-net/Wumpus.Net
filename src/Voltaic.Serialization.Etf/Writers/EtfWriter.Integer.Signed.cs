using System.Buffers;
using System.Buffers.Binary;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, sbyte value, StandardFormat standardFormat)
        {
            if (standardFormat.IsDefault)
            {
                if (/*value <= byte.MaxValue &&*/ value >= byte.MinValue)
                    return TryWrite(ref writer, (byte)value, standardFormat);

                if (value >= 0)
                {
                    writer.Push((byte)EtfTokenType.SmallInteger);
                    writer.Push((byte)value);
                }
                else
                {
                    writer.Push((byte)EtfTokenType.Integer);
                    BinaryPrimitives.WriteInt32BigEndian(writer.GetSpan(4), value);
                    writer.Advance(4);
                }
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

        public static bool TryWrite(ref ResizableMemory<byte> writer, short value, StandardFormat standardFormat)
        {
            if (standardFormat.IsDefault)
            {
                if (value <= byte.MaxValue && value >= byte.MinValue)
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

        public static bool TryWrite(ref ResizableMemory<byte> writer, int value, StandardFormat standardFormat)
        {
            if (standardFormat.IsDefault)
            {
                if (value <= byte.MaxValue && value >= byte.MinValue)
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

        public static bool TryWrite(ref ResizableMemory<byte> writer, long value, StandardFormat standardFormat)
        {
            if (standardFormat.IsDefault)
            {
                if (value <= byte.MaxValue && value >= byte.MinValue)
                    return TryWrite(ref writer, (byte)value, standardFormat);
                if (value <= int.MaxValue && value >= int.MinValue)
                    return TryWrite(ref writer, (int)value, standardFormat);

                writer.Push((byte)EtfTokenType.SmallBig);
                writer.Push(8);
                if (value >= 0)
                {
                    writer.Push(0);
                    BinaryPrimitives.WriteUInt64LittleEndian(writer.GetSpan(8), (ulong)value);
                }
                else
                {
                    writer.Push(1);
                    if (value == long.MinValue)
                        BinaryPrimitives.WriteUInt64LittleEndian(writer.GetSpan(8), 9223372036854775808);
                    else
                        BinaryPrimitives.WriteUInt64LittleEndian(writer.GetSpan(8), (ulong)-value);
                }
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
