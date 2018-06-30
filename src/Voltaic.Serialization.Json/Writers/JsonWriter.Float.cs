using System.Buffers;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, float value, StandardFormat standardFormat = default)
        {
            if (standardFormat.IsDefault && !float.IsInfinity(value) && !float.IsNaN(value))
            {
                if (!Utf8Writer.TryWrite(ref writer, value, JsonSerializer.FloatFormat.Symbol))
                    return false;
            }
            else
            {
                writer.Push((byte)'"');
                if (!Utf8Writer.TryWrite(ref writer, value, !standardFormat.IsDefault ? standardFormat : JsonSerializer.FloatFormat.Symbol))
                    return false;
                writer.Push((byte)'"');
            }
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, double value, StandardFormat standardFormat = default)
        {
            if (standardFormat.IsDefault && !double.IsInfinity(value) && !double.IsNaN(value))
            {
                if (!Utf8Writer.TryWrite(ref writer, value, JsonSerializer.FloatFormat.Symbol))
                    return false;
            }
            else
            {
                writer.Push((byte)'"');
                if (!Utf8Writer.TryWrite(ref writer, value, !standardFormat.IsDefault ? standardFormat  : JsonSerializer.FloatFormat.Symbol))
                    return false;
                writer.Push((byte)'"');
            }
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, decimal value, StandardFormat standardFormat = default)
        {
            writer.Push((byte)'"');
            if (!Utf8Writer.TryWrite(ref writer, value, standardFormat))
                return false;
            writer.Push((byte)'"');
            return true;
        }
    }
}
