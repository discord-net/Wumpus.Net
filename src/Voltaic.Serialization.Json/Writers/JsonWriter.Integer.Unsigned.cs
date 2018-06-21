using System.Buffers;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, byte value, StandardFormat standardFormat)
        {
            if (standardFormat.Symbol != JsonSerializer.IntFormat.Symbol)
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

        public static bool TryWrite(ref ResizableMemory<byte> writer, ushort value, StandardFormat standardFormat)
        {
            if (standardFormat.Symbol != JsonSerializer.IntFormat.Symbol)
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

        public static bool TryWrite(ref ResizableMemory<byte> writer, uint value, StandardFormat standardFormat)
        {
            if (standardFormat.Symbol != JsonSerializer.IntFormat.Symbol)
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

        public static bool TryWrite(ref ResizableMemory<byte> writer, ulong value, StandardFormat standardFormat, bool useQuotes = true)
        {
            if (useQuotes || standardFormat.Symbol != JsonSerializer.IntFormat.Symbol)
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
