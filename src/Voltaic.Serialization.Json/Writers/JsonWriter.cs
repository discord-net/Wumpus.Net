﻿using System.Buffers.Binary;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonWriter
    {
        public static bool TryWriteNull(ref ResizableMemory<byte> writer)
        {
            var data = writer.CreateBuffer(4); // null
            const uint nullValue = ('n' << 24) + ('u' << 16) + ('l' << 8) + ('l' << 0);
            BinaryPrimitives.TryWriteUInt32BigEndian(data, nullValue);
            writer.Write(data, 4);
            return true;
        }
    }
}

