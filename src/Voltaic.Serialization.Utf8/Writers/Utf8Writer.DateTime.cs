using System;
using System.Buffers;
using System.Buffers.Text;

namespace Voltaic.Serialization.Utf8
{
    public static partial class Utf8Writer
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, DateTime value, StandardFormat standardFormat)
        {
            var data = writer.GetSpan(23); // 9999-12-31T11:59:59.999
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten, standardFormat))
                return false;
            writer.Advance(bytesWritten);
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, DateTimeOffset value, StandardFormat standardFormat)
        {
            var data = writer.GetSpan(33); // 9999-12-31T11:59:59.999999+00:00
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten, standardFormat))
                return false;
            writer.Advance(bytesWritten);
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, TimeSpan value, StandardFormat standardFormat)
        {
            var data = writer.GetSpan(26); // -10675199.02:48:05.4775808
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten, standardFormat))
                return false;
            writer.Advance(bytesWritten);
            return true;
        }
    }
}
