using System;
using System.Buffers.Text;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, Guid value)
        {
            var data = writer.CreateBuffer(40);  // "{ABCDEFGH-ABCD-ABCD-ABCD-ABCDEFGHIJKL}"
            data[0] = (byte)'"';
            if (!Utf8Formatter.TryFormat(value, data.Slice(1), out int bytesWritten))
                return false;
            data[bytesWritten + 1] = (byte)'"';
            writer.Write(data.Slice(0, bytesWritten + 2));
            return true;
        }
    }
}
