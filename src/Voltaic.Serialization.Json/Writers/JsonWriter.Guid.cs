using System;
using System.Buffers.Text;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, Guid value)
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
