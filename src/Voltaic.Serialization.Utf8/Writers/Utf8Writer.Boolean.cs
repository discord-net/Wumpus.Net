using System.Buffers.Text;

namespace Voltaic.Serialization.Utf8
{
    public static partial class Utf8Writer
    {
        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, bool value)
        {
            var data = writer.GetSpan(5); // False
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }
    }
}
