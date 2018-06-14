using System;

namespace Voltaic.Serialization
{
    public abstract class ValueConverter<TValue>
    {
        public abstract bool TryRead(
            Serializer serializer,
            ref ReadOnlySpan<byte> data,
            out TValue result,
            PropertyMap propMap = null);

        public abstract bool TryWrite(
            Serializer serializer,
            ref MemoryBufferWriter<byte> writer,
            TValue value,
            PropertyMap propMap = null);
    }
}
