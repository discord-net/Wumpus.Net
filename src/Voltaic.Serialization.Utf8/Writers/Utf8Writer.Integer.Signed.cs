using System.Buffers;
using System.Buffers.Text;

namespace Voltaic.Serialization.Utf8
{
    public static partial class Utf8Writer
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, sbyte value, StandardFormat standardFormat)
        {
            var data = writer.CreateBuffer(4); // -256
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten, standardFormat))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, short value, StandardFormat standardFormat)
        {
            var data = writer.CreateBuffer(6); // -32768
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten, standardFormat))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, int value, StandardFormat standardFormat)
        {
            var data = writer.CreateBuffer(11); // -2147483648
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten, standardFormat))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, long value, StandardFormat standardFormat)
        {
            var data = writer.CreateBuffer(20); // -9223372036854775808
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten, standardFormat))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }
    }
}
