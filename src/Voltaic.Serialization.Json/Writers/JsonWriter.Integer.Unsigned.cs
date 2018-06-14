using System.Buffers.Text;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonWriter
    {
        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, byte value)
        {
            var data = writer.GetSpan(3); // 255
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
            {
                DebugLog.WriteFailure("Utf8Formatter failed");
                return false;
            }
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, ushort value)
        {
            var data = writer.GetSpan(5); // 65536
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
            {
                DebugLog.WriteFailure("Utf8Formatter failed");
                return false;
            }
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, uint value)
        {
            var data = writer.GetSpan(10); // 4294967295
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
            {
                DebugLog.WriteFailure("Utf8Formatter failed");
                return false;
            }
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, ulong value, bool useQuotes = true)
        {
            if (useQuotes)
            {
                var data = writer.GetSpan(22); // "18446744073709551615"
                data[0] = (byte)'"';
                if (!Utf8Formatter.TryFormat(value, data.Slice(1), out int bytesWritten))
                {
                    DebugLog.WriteFailure("Utf8Formatter failed");
                    return false;
                }
                data[bytesWritten + 1] = (byte)'"';
                writer.Write(data.Slice(0, bytesWritten));
            }
            else
            {
                var data = writer.GetSpan(20); // 18446744073709551615
                if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                {
                    DebugLog.WriteFailure("Utf8Formatter failed");
                    return false;
                }
                writer.Write(data.Slice(0, bytesWritten));
            }
            return true;
        }
    }
}
