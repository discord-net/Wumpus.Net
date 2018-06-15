using System;
using System.Buffers;
using System.Buffers.Text;

namespace Voltaic.Serialization.Utf8
{
    public static partial class Utf8Writer
    {
        private static readonly StandardFormat _dateTimeFormat = new StandardFormat('O');

        public static bool TryWrite(ref ResizableMemory<byte> writer, DateTime value)
        {
            var data = writer.CreateBuffer(23); // 9999-12-31T11:59:59.999
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten, _dateTimeFormat))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, DateTimeOffset value)
        {
            var data = writer.CreateBuffer(29); // 9999-12-31T11:59:59.999+00:00
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten, _dateTimeFormat))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, TimeSpan value)
        {
            var data = writer.CreateBuffer(26); // -10675199.02:48:05.4775808
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }
    }
}
