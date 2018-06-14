using System;
using System.Buffers;
using System.Buffers.Text;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonWriter
    {
        private static readonly StandardFormat _dateTimeFormat = new StandardFormat('O');

        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, DateTime value)
        {
            var data = writer.GetSpan(23); // 9999-12-31T11:59:59.999
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten, _dateTimeFormat))
            {
                DebugLog.WriteFailure("Utf8Formatter failed");
                return false;
            }
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, DateTimeOffset value)
        {
            var data = writer.GetSpan(29); // 9999-12-31T11:59:59.999+00:00
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten, _dateTimeFormat))
            {
                DebugLog.WriteFailure("Utf8Formatter failed");
                return false;
            }
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, TimeSpan value)
        {
            var data = writer.GetSpan(26); // -10675199.02:48:05.4775808
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
            {
                DebugLog.WriteFailure("Utf8Formatter failed");
                return false;
            }
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }
    }
}
