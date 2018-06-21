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
                writer.Push((byte)'"');
                if (!Utf8Writer.TryWrite(ref writer, value, standardFormat))
                    return false;
                writer.Push((byte)'"');
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

