using System.Buffers;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, float value, StandardFormat standardFormat)
        {
            if (standardFormat.Symbol != JsonSerializer.FloatFormat.Symbol || float.IsInfinity(value) || float.IsNaN(value))
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

        public static bool TryWrite(ref ResizableMemory<byte> writer, double value, StandardFormat standardFormat)
        {
            if (standardFormat.Symbol != JsonSerializer.FloatFormat.Symbol || double.IsInfinity(value) || double.IsNaN(value))
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

        public static bool TryWrite(ref ResizableMemory<byte> writer, decimal value, StandardFormat standardFormat)
        {
            if (standardFormat.Symbol != JsonSerializer.FloatFormat.Symbol)
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
