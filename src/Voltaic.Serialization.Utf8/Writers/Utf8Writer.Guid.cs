using System;
using System.Buffers;
using System.Buffers.Text;

namespace Voltaic.Serialization.Utf8
{
    public static partial class Utf8Writer
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, Guid value, StandardFormat standardFormat)
        {
            var data = writer.CreateBuffer(38); // {ABCDEFGH-ABCD-ABCD-ABCD-ABCDEFGHIJKL}
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten, standardFormat))
                return false;
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }
    }
}
