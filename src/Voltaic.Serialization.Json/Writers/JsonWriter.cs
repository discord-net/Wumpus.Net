using System.Buffers.Binary;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonWriter
    {
        public static bool TryWriteNull(ref ResizableMemory<byte> writer)
        {
            var data = writer.GetSpan(4); // null
            const uint nullValue = ('n' << 24) + ('u' << 16) + ('l' << 8) + ('l' << 0);
            BinaryPrimitives.WriteUInt32BigEndian(data, nullValue);
            writer.Advance(4);
            return true;
        }
    }
}

