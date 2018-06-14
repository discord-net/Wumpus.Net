using System.Buffers.Text;

namespace Voltaic.Serialization.Utf8
{
    public static partial class Utf8Writer
    {
        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, sbyte value)
        {
            var data = writer.GetSpan(4); // -256
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, short value)
        {
            var data = writer.GetSpan(6); // -32768
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, int value)
        {
            var data = writer.GetSpan(11); // -2147483648
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, long value)
        {
            var data = writer.GetSpan(20); // -9223372036854775808
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }
    }
}
