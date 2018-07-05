using System.Buffers;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, bool value, StandardFormat standardFormat = default)
        {
            if (standardFormat.IsDefault)
            {
                if (!Utf8Writer.TryWrite(ref writer, value, JsonSerializer.BooleanFormat.Symbol))
                    return false;
            }
            else
            {
                writer.Push((byte)'"');
                if (!Utf8Writer.TryWrite(ref writer, value, standardFormat))
                    return false;
                writer.Push((byte)'"');
            }
            return true;
        }
    }
}

