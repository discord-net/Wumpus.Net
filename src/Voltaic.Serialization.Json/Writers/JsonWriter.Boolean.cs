using System.Buffers;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, bool value, StandardFormat standardFormat)
        {
            if (standardFormat.Symbol != JsonSerializer.BooleanFormat.Symbol)
            {
                writer.Append((byte)'"');
                if (!Utf8Writer.TryWrite(ref writer, value, standardFormat))
                    return false;
                writer.Append((byte)'"');
            }
            else
            {
                if (!Utf8Writer.TryWrite(ref writer, value, standardFormat))
                    return false;
            }
            return true;
        }
    }
}

