using System.Buffers;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, sbyte value, StandardFormat standardFormat)
        {
            if (standardFormat.Symbol != JsonSerializer.IntFormat.Symbol)
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

        public static bool TryWrite(ref ResizableMemory<byte> writer, short value, StandardFormat standardFormat)
        {
            if (standardFormat.Symbol != JsonSerializer.IntFormat.Symbol)
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

        public static bool TryWrite(ref ResizableMemory<byte> writer, int value, StandardFormat standardFormat)
        {
            if (standardFormat.Symbol != JsonSerializer.IntFormat.Symbol)
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

        public static bool TryWrite(ref ResizableMemory<byte> writer, long value, StandardFormat standardFormat, bool useQuotes = true)
        {
            if (useQuotes || standardFormat.Symbol != JsonSerializer.IntFormat.Symbol)
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
