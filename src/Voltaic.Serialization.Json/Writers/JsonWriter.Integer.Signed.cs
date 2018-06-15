using System.Buffers.Text;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, sbyte value)
        {
            var data = writer.CreateBuffer(4); // -256
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, short value)
        {
            var data = writer.CreateBuffer(6); // -32768
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, int value)
        {
            var data = writer.CreateBuffer(11); // -2147483648
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, long value, bool useQuotes = true)
        {
            if (useQuotes)
            {
                var data = writer.CreateBuffer(22); // "-9223372036854775808"
                data[0] = (byte)'"';
                if (!Utf8Formatter.TryFormat(value, data.Slice(1), out int bytesWritten))
                    return false;
                data[bytesWritten + 1] = (byte)'"';
                writer.Write(data.Slice(0, bytesWritten + 2));
            }
            else
            {
                var data = writer.CreateBuffer(20); // -9223372036854775808
                if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                    return false;
                writer.Write(data.Slice(0, bytesWritten));
            }
            return true;
        }
    }
}
