using System.Buffers;
using System.Buffers.Text;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonWriter
    {
        private static readonly StandardFormat _boolFormat = new StandardFormat('l');

        public static bool TryWrite(ref MemoryBufferWriter<byte> writer, bool value)
        {
            var data = writer.GetSpan(5); // False
            if (!Utf8Formatter.TryFormat(value, data, out int bytesWritten, _boolFormat))
            {
                DebugLog.WriteFailure("Utf8Formatter failed");
                return false;
            }
            writer.Write(data.Slice(0, bytesWritten));
            return true;
        }
    }
}

