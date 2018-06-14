using System;
using System.Buffers.Text;

namespace Voltaic.Serialization.Utf8
{
    public static partial class Utf8Writer
    {
        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, Guid value)
        {
            var data = writer.GetSpan(26); // -10675199.02:48:05.4775808
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten))
            {
                DebugLog.WriteFailure("Utf8Formatter failed");
                return false;
            }
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }
    }
}
