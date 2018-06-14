using System.Buffers.Text;

namespace Voltaic.Serialization.Utf8
{
    public static partial class Utf8Writer
    {
        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, byte value)
        {
            var data = writer.GetSpan(3); // 255
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, ushort value)
        {
            var data = writer.GetSpan(5); // 65536
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, uint value)
        {
            var data = writer.GetSpan(10); // 4294967295
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, ulong value)
        {
            var data = writer.GetSpan(20); // 18446744073709551615
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }
    }
}
