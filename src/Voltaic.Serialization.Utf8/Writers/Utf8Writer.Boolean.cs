using System.Buffers.Text;

namespace Voltaic.Serialization.Utf8
{
    public static partial class Utf8Writer
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, bool value)
        {
            var data = writer.CreateBuffer(5); // False
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }
    }
}
