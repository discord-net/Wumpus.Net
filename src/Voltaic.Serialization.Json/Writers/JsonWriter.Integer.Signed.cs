using System.Buffers.Text;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonWriter
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

        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, long value, bool useQuotes = true)
        {
            if (useQuotes)
            {
                var data = writer.GetSpan(22); // "-9223372036854775808"
                data[0] = (byte)'"';
                if (!Utf8Formatter.TryFormat(value, data.Slice(1), out int bytesWritten))
                    return false;
                data[bytesWritten + 1] = (byte)'"';
                writer.Write(data.Slice(0, bytesWritten));
            }
            else
            {
                var data = writer.GetSpan(20); // -9223372036854775808
                if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                    return false;
                writer.Write(data.Slice(0, bytesWritten));
            }
            return true;
        }
    }
}
