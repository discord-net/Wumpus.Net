using System.Buffers;
using System.Buffers.Text;

namespace Voltaic.Serialization.Utf8
{
    public static partial class Utf8Writer
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, byte value, StandardFormat standardFormat)
        {
            var data = writer.GetSpan(3); // 255
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten, standardFormat))
                return false;
            writer.Advance(bytesWritten);
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, ushort value, StandardFormat standardFormat)
        {
            var data = writer.GetSpan(5); // 65536
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten, standardFormat))
                return false;
            writer.Advance(bytesWritten);
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, uint value, StandardFormat standardFormat)
        {
            var data = writer.GetSpan(10); // 4294967295
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten, standardFormat))
                return false;
            writer.Advance(bytesWritten);
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, ulong value, StandardFormat standardFormat)
        {
            var data = writer.GetSpan(20); // 18446744073709551615
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten, standardFormat))
                return false;
            writer.Advance(bytesWritten);
            return true;
        }
    }
}
