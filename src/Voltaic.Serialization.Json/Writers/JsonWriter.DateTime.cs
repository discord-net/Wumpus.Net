using System;
using System.Buffers;
using System.Buffers.Text;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonWriter
    {
        private static readonly StandardFormat _dateTimeFormat = new StandardFormat('O');

        public static bool TryWrite(ref ResizableMemory<byte> writer, DateTime value)
        {
            var data = writer.CreateBuffer(25); // "9999-12-31T11:59:59.999"
            data[0] = (byte)'"';
            if (!Utf8Formatter.TryFormat(value, data.Slice(1), out int bytesWritten, _dateTimeFormat))
                return false;
            data[bytesWritten + 1] = (byte)'"';
            writer.Write(data.Slice(0, bytesWritten + 2));
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, DateTimeOffset value)
        {
            var data = writer.CreateBuffer(31); // "9999-12-31T11:59:59.999+00:00"
            data[0] = (byte)'"';
            if (!Utf8Formatter.TryFormat(value, data.Slice(1), out int bytesWritten, _dateTimeFormat))
                return false;
            data[bytesWritten + 1] = (byte)'"';
            writer.Write(data.Slice(0, bytesWritten + 2));
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, TimeSpan value)
        {
            var data = writer.CreateBuffer(28); // "-10675199.02:48:05.4775808"
            data[0] = (byte)'"';
            if (!Utf8Formatter.TryFormat(value, data.Slice(1), out int bytesWritten))
                return false;
            data[bytesWritten + 1] = (byte)'"';
            writer.Write(data.Slice(0, bytesWritten + 2));
            return true;
        }
    }
}
