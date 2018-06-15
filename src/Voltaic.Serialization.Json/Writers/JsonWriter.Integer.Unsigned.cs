using System.Buffers.Text;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, byte value)
        {
            var data = writer.CreateBuffer(3); // 255
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, ushort value)
        {
            var data = writer.CreateBuffer(5); // 65536
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, uint value)
        {
            var data = writer.CreateBuffer(10); // 4294967295
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, ulong value, bool useQuotes = true)
        {
            if (useQuotes)
            {
                var data = writer.CreateBuffer(22); // "18446744073709551615"
                data[0] = (byte)'"';
                if (!Utf8Formatter.TryFormat(value, data.Slice(1), out int bytesWritten))
                    return false;
                data[bytesWritten + 1] = (byte)'"';
                writer.Write(data.Slice(0, bytesWritten + 2));
            }
            else
            {
                var data = writer.CreateBuffer(20); // 18446744073709551615
                if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                    return false;
                writer.Write(data.Slice(0, bytesWritten));
            }
            return true;
        }
    }
}
