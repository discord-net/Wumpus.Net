using System.Buffers;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, byte value, StandardFormat standardFormat = default)
        {
            if (standardFormat.IsDefault)
            {
                if (!Utf8Writer.TryWrite(ref writer, value, JsonSerializer.IntFormat.Symbol))
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

        public static bool TryWrite(ref ResizableMemory<byte> writer, ushort value, StandardFormat standardFormat = default)
        {
            if (standardFormat.IsDefault)
            {
                if (!Utf8Writer.TryWrite(ref writer, value, JsonSerializer.IntFormat.Symbol))
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

        public static bool TryWrite(ref ResizableMemory<byte> writer, uint value, StandardFormat standardFormat = default)
        {
            if (standardFormat.IsDefault)
            {
                if (!Utf8Writer.TryWrite(ref writer, value, JsonSerializer.IntFormat.Symbol))
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

        public static bool TryWrite(ref ResizableMemory<byte> writer, ulong value, StandardFormat standardFormat = default)
        {
            if (standardFormat.IsDefault)
            {
                if (!Utf8Writer.TryWrite(ref writer, value, JsonSerializer.IntFormat.Symbol))
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
